#include "pch.h"

#include <iostream>
#include <chrono>

#include "mkl.h"

enum VMf : MKL_INT {
	Sin = 0,
	Cos,
	SinCos
};

extern "C" _declspec(dllexport)
void CallMKLFunction(int function_code, const MKL_INT n, const double *points, double *timings,
				     double &max_error, double &max_error_arg, double *max_error_func_values, int& ret)
{
	double* results = new double[n];
	
	// TODO: find out why calls are failing and add proper code here
	switch (function_code) {
	case VMf::Sin:
		break;
	case VMf::Cos:
		break;
	case VMf::SinCos:
		break;
	}

	// TODO: Dummy code, remove later
	for (size_t i = 0; i < 3; i++) {
		timings[i] = (2 - i) * n / 2;
	}
	max_error = (double) n;
	max_error_arg = n > 0 ? n - 1 : 0;
	for (size_t i = 0; i < 3; i++) {
		max_error_func_values[i] = i * n / 2;
	}
	ret = 0;
	
	delete[] results;
}