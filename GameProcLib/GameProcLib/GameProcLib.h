#ifndef GAME_PROC_LIB_H
#define GAME_PROC_LIB_H

//--------------------------------------
//--------------------------------------

#include <memory>
#include <vector>

//--------------------------------------
//--------------------------------------

class Resource {
private:
	int player;
	int resourceName;
public:
	Resource(int resourceName, int player);
	int getPlayer() const;
	int getResourceName() const;
};

//--------------------------------------
//--------------------------------------

class GamePiece : public Resource {
private:
public:
	typedef std::shared_ptr<GamePiece> ptr;

	GamePiece(int resourceName, int player);
};

//--------------------------------------
//--------------------------------------

class Move {
private:
	int player;
	int newStateEvaluation;
	GamePiece::ptr doer;
public:
	typedef std::shared_ptr<Move> ptr;
	
	Move(int player, GamePiece::ptr doer);
	int getPlayer() const;
	int getNewStateEvaluation() const;
	void setNewStateEvaluation(int evalValue);

	inline bool operator < (const Move& other) const;
};

//--------------------------------------
//--------------------------------------

class Cell {
private:
	int index;
	GamePiece::ptr owner;
public:
	int getIndex() const;
	void setIndex(int index);

	GamePiece::ptr getOwner() const;
	void setOwner(GamePiece::ptr owner);
};

//--------------------------------------
//--------------------------------------

class FieldState {
protected: 
	std::vector<Cell> field;

public:
	typedef std::shared_ptr<FieldState> ptr;

	virtual void notifyEvaluationStarted() = 0;
	virtual void notifyEvaluationEnded() = 0;
	virtual std::vector<Move::ptr> getAllPossibleMoves() = 0;
	virtual FieldState::ptr doMove(Move::ptr move) = 0;
	virtual FieldState::ptr undoMove(Move::ptr move) = 0;
	virtual int evaluate() = 0;
	
	virtual bool isGameEnd() = 0;
};

//--------------------------------------
//--------------------------------------

class GameProcessor {
private:
	FieldState::ptr currentState;
	int minimaxTreeDepth;

	int evaluateTreeNode(FieldState::ptr state, bool max, int a, int b, int depth) const;
public:
	GameProcessor(FieldState::ptr initalState, int minimaxTreeDepth);
	void doMove(Move::ptr move);
	std::vector<Move::ptr> evaluatePossibleMoves() const;

	friend bool operator < (Move::ptr m1, Move::ptr m2);
};

//--------------------------------------
//--------------------------------------

#endif