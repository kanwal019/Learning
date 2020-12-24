#pragma once
#include "CustomMathEnums.h"

class CustomMath
{
public:
	CustomMath(long input) {
		this->inputValue = input;
	}

	~CustomMath(void) {}

	long getValue(MethodType selectedMethod);

private:
	long inputValue;
	MethodType selectedMethod;
	long square();
	long squareRoot();
	long factorial();
	long mod();
};

