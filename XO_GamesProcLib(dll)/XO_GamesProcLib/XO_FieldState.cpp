#include "stdafx.h"
#include "XO_FieldState.h"
#include "XO_Move.h"
#include <algorithm>

XO_FieldState::XO_FieldState(int w, int h) : w(w), h(h) {
	field.resize(w*h);
	for (size_t i = 0; i < field.size(); ++i) {
		field[i].setIndex(i);
	}
	currentPlayer = FIRST_PLAYER;
	evaluationInProgress = false;

	int middle = ceil((double)w / 2);
	possibleMoves[middle * w + middle] = std::make_shared<XO_Move>(NO_PLAYER, std::shared_ptr<GamePiece>(), middle * w + middle);
	possibleMoves[middle * w + middle]->setNewStateEvaluation(1);
}

void XO_FieldState::notifyEvaluationStarted(){
	evaluationInProgress = true;
	playerWhoStartedEval = currentPlayer;
}
void XO_FieldState::notifyEvaluationEnded() {
	evaluationInProgress = false;
	std::swap(savedLineChanges, std::stack<std::shared_ptr<std::vector<LineChange>>>());
	std::swap(savedMoveChanges, std::stack<std::shared_ptr<std::unordered_map<int, int>>>());
}

std::vector<Move::ptr> XO_FieldState::getAllPossibleMoves() {
	std::vector<Move::ptr> moves(possibleMoves.size());

	int i = 0;
	for (std::unordered_map<int, Move::ptr>::iterator it = possibleMoves.begin(); it != possibleMoves.end(); ++it) {
		moves[i++] = it->second;
	}
	std::sort(moves.begin(), moves.end());
	int maxNumOfMoves = 30;
	if (moves.size() > maxNumOfMoves)
		return std::vector<Move::ptr>(moves.begin(), moves.begin() + maxNumOfMoves);
	return moves;
}

int XO_FieldState::getStartCell(int moveCell, int lineNum, int direction) {
	for (int i = 0; i < lineNum; ++i) {
		if (moveCell < 0)
			break;
		switch (direction) {
		case ZUID_OST:
			moveCell = shift(moveCell, NORD_WEST);
			break;
		case ZUID:
			moveCell = shift(moveCell, NORD);
			break;
		case ZUID_WEST:
			moveCell = shift(moveCell, NORD_OST);
			break;
		case WEST:
			moveCell = shift(moveCell, OST);
			break;
		}
	}
	return moveCell;
}

int XO_FieldState::shift(int cell, int direction) {
	int cellW = cell % w;
	int cellH = cell / w;
	switch (direction) {
	case ZUID_OST:
		cellW += 1;
		cellH += 1;
		break;
	case ZUID:
		cellW += 0;
		cellH += 1;
		break;
	case ZUID_WEST:
		cellW -= 1;
		cellH += 1;
		break;
	case WEST:
		cellW -= 1;
		cellH += 0;
		break;
	case NORD_WEST:
		cellW -= 1;
		cellH -= 1;
		break;
	case NORD:
		cellW -= 0;
		cellH -= 1;
		break;
	case NORD_OST:
		cellW += 1;
		cellH -= 1;
		break;
	case OST:
		cellW += 1;
		cellH -= 0;
		break;
	}
	if (cellW < 0 || cellW >= w || cellH < 0 || cellH >= h)
		return -1;
	return cellW + cellH * w;
}

FieldState::ptr XO_FieldState::doMove(Move::ptr move) { 
	XO_Move* xoMove = (XO_Move*) move.get();
	std::shared_ptr<std::vector<LineChange>> lineChanges = std::make_shared<std::vector<LineChange>>();
	std::shared_ptr<std::unordered_map<int, int>> moveChanges = std::make_shared<std::unordered_map<int, int>>();

	int cell;
	int moveCell = xoMove->getIndexTo();
	for (int dir = ZUID_OST; dir <= WEST; ++dir) {
		for (int i = 0; i < 5; ++i) { //lines
			Line line;
			bool lineIsOk = true;
			cell = getStartCell(moveCell, i, dir);
			for (int j = 0; j < 5; ++j) { //cells in line
				if (cell < 0) {
					lineIsOk = false;
					break;
				}
				if (field[cell].getOwner()) {
					if (field[cell].getOwner()->getPlayer() == FIRST_PLAYER)
						++line.firstPlayerNum;
					else
						++line.secondPlayerNum;
				}
				line.cells[j] = cell;
				cell = shift(cell, dir);
			}

			if (lineIsOk) {
				LineChange lc;
				lc.player = currentPlayer;
				if (line.firstPlayerNum > 0 && line.secondPlayerNum > 0) {
					lc.isAlive = false;
				}
				else {
					if (line.firstPlayerNum > 0) {
						lc.player = FIRST_PLAYER;
						lc.len = line.firstPlayerNum;
					}
					if (line.secondPlayerNum > 0) {
						lc.player = SECOND_PLAYER;
						lc.len = line.secondPlayerNum;
					}
				}
				if (lc.isAlive) {
					if (lc.player != currentPlayer) {
						lc.isAlive = false;
					}
					lineChanges->push_back(lc);
					for (int c = 0; c < 5; ++c) {
						int index = line.cells[c];
						if (!field[index].getOwner() && index != moveCell && (*moveChanges)[index] != INT_MAX) {
							if (lc.len > 0)
								(*moveChanges)[index] -= std::pow(10, 2 * (lc.len - 1));
							if (lc.isAlive) {
								(*moveChanges)[index] += std::pow(10, 2 * (lc.len));
							}
						}
					}
				}
			}
		}
	}
	if (!possibleMoves[moveCell])
		possibleMoves[moveCell] = std::make_shared<XO_Move>(NO_PLAYER, std::shared_ptr<GamePiece>(), moveCell);
	(*moveChanges)[moveCell] = -possibleMoves[moveCell]->getNewStateEvaluation();

	savedLineChanges.push(lineChanges);
	savedMoveChanges.push(moveChanges);

	for (size_t i = 0; i < lineChanges->size(); ++i) {
		if ((*lineChanges)[i].isAlive) {
			if (currentPlayer == FIRST_PLAYER)
				++lines.firstPlayer[(*lineChanges)[i].len];
			else
				++lines.secondPlayer[(*lineChanges)[i].len];
			if ((*lineChanges)[i].len > 0) {
				if (currentPlayer == FIRST_PLAYER)
					--lines.firstPlayer[(*lineChanges)[i].len - 1];
				else
					--lines.secondPlayer[(*lineChanges)[i].len - 1];
			}
		}
		else {
			if (currentPlayer == FIRST_PLAYER)
				--lines.secondPlayer[(*lineChanges)[i].len - 1];
			else 
				--lines.firstPlayer[(*lineChanges)[i].len - 1];
		}
		
	}

	for (std::unordered_map<int, int>::iterator it = moveChanges->begin(); it != moveChanges->end(); ++it) {
		Move::ptr move = possibleMoves[it->first];
		if (!move) {
			possibleMoves[it->first] = move = std::make_shared<XO_Move>(NO_PLAYER, std::shared_ptr<GamePiece>(), it->first);
		}
		move->setNewStateEvaluation(move->getNewStateEvaluation() + it->second);
		if (move->getNewStateEvaluation() == 0) {
			possibleMoves.erase(it->first);
		}
	}

	field[xoMove->getIndexTo()].setOwner(std::make_shared<GamePiece>(0, currentPlayer));
	currentPlayer = currentPlayer == FIRST_PLAYER ? SECOND_PLAYER : FIRST_PLAYER;

	return shared_from_this();
}

FieldState::ptr XO_FieldState::undoMove(Move::ptr move) { 
	XO_Move* xoMove = (XO_Move*)move.get();

	std::shared_ptr<std::vector<LineChange>> lineChanges = savedLineChanges.top();
	std::shared_ptr<std::unordered_map<int, int>> moveChanges = savedMoveChanges.top();
	savedMoveChanges.pop();
	savedLineChanges.pop();

	int prevPlayer = currentPlayer == FIRST_PLAYER ? SECOND_PLAYER : FIRST_PLAYER;
	for (size_t i = 0; i < lineChanges->size(); ++i) {
		if ((*lineChanges)[i].isAlive) {
			if (prevPlayer == FIRST_PLAYER)
				--lines.firstPlayer[(*lineChanges)[i].len];
			else
				--lines.secondPlayer[(*lineChanges)[i].len];
			if ((*lineChanges)[i].len > 0) {
				if (prevPlayer == FIRST_PLAYER)
					++lines.firstPlayer[(*lineChanges)[i].len - 1];
				else
					++lines.secondPlayer[(*lineChanges)[i].len - 1];
			}
		}
		else {
			if (prevPlayer == FIRST_PLAYER)
				++lines.secondPlayer[(*lineChanges)[i].len - 1];
			else
				++lines.firstPlayer[(*lineChanges)[i].len - 1];
		}

	}

	for (std::unordered_map<int, int>::iterator it = moveChanges->begin(); it != moveChanges->end(); ++it) {
		Move::ptr move = possibleMoves[it->first];
		if (!move) {
			possibleMoves[it->first] = move = std::make_shared<XO_Move>(NO_PLAYER, std::shared_ptr<GamePiece>(), it->first);
		}
		move->setNewStateEvaluation(move->getNewStateEvaluation() - it->second);
		if (move->getNewStateEvaluation() == 0)
			possibleMoves.erase(it->first);
	}

	field[xoMove->getIndexTo()].setOwner(GamePiece::ptr());
	currentPlayer = currentPlayer == FIRST_PLAYER ? SECOND_PLAYER : FIRST_PLAYER;

	return shared_from_this();
}

int XO_FieldState::evaluate() {
	int* currPlayerStats;
	int* enemyStats;
	if (playerWhoStartedEval == FIRST_PLAYER) {
		currPlayerStats = lines.firstPlayer;
		enemyStats = lines.secondPlayer;
	}
	else {
		currPlayerStats = lines.secondPlayer;
		enemyStats = lines.firstPlayer;
	}

	if (currPlayerStats[4] > 0)
		return INT_MAX;
	if (enemyStats[4] > 0)
		return INT_MIN;

	int resultingEval = 0;
	for (int i = 0; i < 5; ++i){
		resultingEval += currPlayerStats[i] * std::pow(10, 2*i);
		resultingEval -= enemyStats[i] * std::pow(10, 2 * i);
	}

	return resultingEval;
}

bool XO_FieldState::isGameEnd() { 
	return lines.firstPlayer[4] > 0 || lines.secondPlayer[4] > 0;
}
