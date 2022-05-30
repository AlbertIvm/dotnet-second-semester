#include "pch.h"

#include <iostream>;
#include <chrono>;

#include "mkl.h"

enum VMf : MKL_INT {
	Sin = 0,
	Cos,
	SinCos
};

extern "C" _declspec(dllexport)
void CallMKLFunction(int function_code, int n, double *points, double *timings,
				     double &max_error, double &max_error_arg, double *max_error_func_values, int& ret)
{
	switch (function_code) {
	case VMf::Sin:
		std::cout << "Calling Sin" << std::endl;
		break;
	case VMf::Cos:
		std::cout << "Calling Cos" << std::endl;
		break;
	case VMf::SinCos:
		std::cout << "Calling SinCos" << std::endl;
		break;
	}
}