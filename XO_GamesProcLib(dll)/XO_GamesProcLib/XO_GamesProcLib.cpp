// This is the main DLL file.

#include "stdafx.h"

#include "XO_GamesProcLib.h"

#include "XO_FieldState.h"
#include "XO_Move.h"

XO_GamesProcLib::XO_GameProcessor::XO_GameProcessor(int difficulty) {
	int minimaxTreeDepth;
	switch (difficulty) {
		case 0:
			minimaxTreeDepth = 0;
			break;
		case 1:
			minimaxTreeDepth = 1;
			break;
		case 2:
			minimaxTreeDepth = 3;
			break;
	}

	gameProcessor = new GameProcessor(std::make_shared<XO_FieldState>(36, 36), minimaxTreeDepth);
}

XO_GamesProcLib::XO_GameProcessor::~XO_GameProcessor() {
	if (gameProcessor)
		delete this->gameProcessor;
	this->gameProcessor = NULL;
}

IntPtr XO_GamesProcLib::XO_GameProcessor::evaluatePossibleMoves([System::Runtime::InteropServices::Out] int %resultingMovesNumber) {
	std::vector<Move::ptr> possibleMoves = gameProcessor->evaluatePossibleMoves();
	possibleMovesArray = new int[possibleMoves.size()];
	for (size_t i = 0; i < possibleMoves.size(); ++i) {
		possibleMovesArray[i] = ((XO_Move*)possibleMoves[i].get())->getIndexTo();
	}
	resultingMovesNumber = possibleMoves.size();
	return IntPtr(possibleMovesArray);
}
//should be called after each call of evaluatePossibleMoves()
void XO_GamesProcLib::XO_GameProcessor::freeMemory() {
	if (possibleMovesArray)
		delete[] possibleMovesArray;
	possibleMovesArray = NULL;
}

void XO_GamesProcLib::XO_GameProcessor::doMove(int index) {
	gameProcessor->doMove(std::make_shared<XO_Move>(0, std::shared_ptr<GamePiece>(), index));
}

bool XO_GamesProcLib::XO_GameProcessor::isGameEnd() {
	return gameProcessor->isGameEnd();
}