#include "pch.h"
#include "CustomMathWrapper.h"

extern "C" CMATH_EXPORT CustomMath * new_CustomMathInstance(long input) {
	return new CustomMath(input);
}

extern "C" CMATH_EXPORT void delete_CustomMathInstance(CustomMath * instance) {
	delete instance;
}

extern "C" CMATH_EXPORT long getValue(CustomMath * instance, MethodType selectedMethod) {
	return instance->getValue(selectedMethod);
}