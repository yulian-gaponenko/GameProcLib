#ifndef GAME_PROC_LIB_H
#define GAME_PROC_LIB_H

//--------------------------------------
//--------------------------------------

class Resource;
class GamePiece;
class Move;
class Cell;
class FieldState;
class GameProcessor;

//--------------------------------------
//--------------------------------------

#include <memory>
#include <vector>

//--------------------------------------
//--------------------------------------

class Resource {
private:
	int resourceName;
public:
	Resource(int resourceName);
	int getResourceName() const;
};

//--------------------------------------
//--------------------------------------

class GamePiece : Resource {
private:
public:
	GamePiece(int resourceName);
};

//--------------------------------------
//--------------------------------------

class Move {
private:
	int player;
	int newStateEvaluation;
	std::shared_ptr<GamePiece> doer;
public:
	Move(int player, std::shared_ptr<GamePiece> doer);
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
	std::shared_ptr<GamePiece> owner;
public:
	Cell(int index);
	inline int getIndex() const;

	std::shared_ptr<GamePiece> getOwner() const;
	void setOwner(std::shared_ptr<GamePiece> owner);
};

//--------------------------------------
//--------------------------------------

class FieldState {
private: 
	std::vector<Cell> field;

public:
	FieldState();
	virtual std::vector<std::shared_ptr<Move>> getAllPossibleMoves() const = 0;
	virtual std::shared_ptr<FieldState> doMove(std::shared_ptr<const Move> move) = 0;
	virtual std::shared_ptr<FieldState> undoMove(std::shared_ptr<const Move> move) = 0;
	virtual int evaluate() = 0;
	
	//returns 0 if false, otherwise returns 1-based index of winnnig player
	virtual int isGameEnd() const = 0;
};

//--------------------------------------
//--------------------------------------

class GameProcessor {
private:
	std::shared_ptr<FieldState> currentState;
	int minimaxTreeDepth;

	int evaluateTreeNode(std::shared_ptr<FieldState> state, bool max, int a, int b, int depth) const;
public:
	GameProcessor(std::shared_ptr<FieldState>initalState, int minimaxTreeDepth);
	void doMove(std::shared_ptr<const Move> move);
	std::vector<std::shared_ptr<Move>> evaluatePossibleMoves() const;
};

//--------------------------------------
//--------------------------------------

#endif