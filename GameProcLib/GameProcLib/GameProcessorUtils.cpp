#include "GameProcessorUtils.h"

int GameProcessorUtils::compareMinimax(bool max, int firstVal, int secondVal) {
	return max ? firstVal - secondVal : secondVal - firstVal;
}