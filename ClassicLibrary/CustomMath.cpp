#include "pch.h"
#include "CustomMath.h"

#include <cstdlib>
#include <cmath>

long
CustomMath::getValue(MethodType selectedMethod) {
	switch (selectedMethod) {
	case Square:
		return this->square();
	case SquareRoot:
		return this->squareRoot();
	case Factorial:
		return this->factorial();
	case Mod:
		return this->mod();
	default:
		break;
	}
	return -1;
}

long
CustomMath::square() {
	return this->inputValue * this->inputValue;
}

long
CustomMath::squareRoot() {
	return (long)sqrt(this->inputValue);
}

long
CustomMath::factorial() {
	long factorial = 1;
	for (long i = 1; i <= this->inputValue; i++) {
		factorial *= i;
	}
	return factorial;
}

long
CustomMath::mod() {
	return abs(this->inputValue);
}