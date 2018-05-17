#pragma once

#define USABLE_IN_UNITY __declspec(dllexport) 

extern "C" USABLE_IN_UNITY int RandomIntBounded(int lowBound, int highBound);

