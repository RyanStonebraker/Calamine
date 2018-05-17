#include "UnityPluginEX.h"

#include <stdlib.h>
#include <time.h>

extern "C" int RandomIntBounded(int lowBound, int highBound) {
	srand((unsigned int)time(0));
	return (rand() % (highBound + 1 - lowBound) + lowBound);
}
