#pragma once
#include "CustomMath.h"

#ifdef _WIN32
#define CMATH_EXPORT _declspec(dllexport)
extern "C" CMATH_EXPORT CustomMath * new_CustomMathInstance(long input);
extern "C" CMATH_EXPORT void delete_CustomMathInstance(CustomMath * instance);
extern "C" CMATH_EXPORT long getValue(CustomMath * instance, MethodType selectedMethod);
#endif

class CustomMathWrapper
{

};

