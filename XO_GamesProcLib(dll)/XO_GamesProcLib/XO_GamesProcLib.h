// XO_GamesProcLib.h

#pragma once

#include "GameProcLib.h"

using namespace System;

namespace XO_GamesProcLib {

	public ref class XO_GameProcessor {
	private:
		GameProcessor* gameProcessor;
		int* possibleMovesArray;
	public:
		//difficulty can be 0, 1, 2
		XO_GameProcessor(int difficulty);
		virtual ~XO_GameProcessor();

		IntPtr evaluatePossibleMoves([System::Runtime::InteropServices::Out] int %resultingMovesNumber);
		//should be called after each call of evaluatePossibleMoves()
		void freeMemory();

		void doMove(int index);

		bool isGameEnd();
	};
}
