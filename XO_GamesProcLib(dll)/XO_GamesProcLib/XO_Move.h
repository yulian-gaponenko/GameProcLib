#include "GameProcLib.h"

class XO_Move : public Move {
private:
	int indexTo;
public:
	XO_Move(int player, GamePiece::ptr doer, int indexTo) : 
		Move(player, doer), indexTo(indexTo) {}

	int getIndexTo() const { return indexTo; }
};