#include <unordered_map>
#include <stack>
#include "GameProcLib.h"

class XO_FieldState : FieldState, public std::enable_shared_from_this<XO_FieldState> {
private:
	int w;
	int h;
	int currentPlayer;

	enum { NO_PLAYER = 0, FIRST_PLAYER, SECOND_PLAYER };
	enum {NORD_WEST, NORD, NORD_OST, OST, ZUID_OST, ZUID, ZUID_WEST, WEST};
	struct LinesStatistics{
		LinesStatistics() {
			memset(firstPlayer, 0, sizeof(int) * 5);
			memset(secondPlayer, 0, sizeof(int) * 5);
		}
		int firstPlayer[5];
		int secondPlayer[5];
	};

	struct LineChange {
		bool isAlive = true;
		int player = NO_PLAYER;
		int len = 0; //length that was before changes
	};

	struct Line {
		int firstPlayerNum = 0;
		int secondPlayerNum = 0;
		int cells[5];
	};

	LinesStatistics lines;
	//cell index, move
	std::unordered_map<int, Move::ptr> possibleMoves;
	std::stack<std::vector<LineChange>> savedLineChanges;
	//cell index, moveEvalDelta
	std::stack<std::unordered_map<int, int>> savedMoveChanges;

	int shift(int cell, int direction);
	int getStartCell(int moveCell, int lineNum, int direction);
public:
	XO_FieldState(int w, int h);
	virtual std::vector<Move::ptr> getAllPossibleMoves();
	virtual FieldState::ptr doMove(Move::ptr move);
	virtual FieldState::ptr undoMove(Move::ptr move);
	virtual int evaluate();

	virtual bool isGameEnd();
};