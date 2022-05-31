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
	double* results_sin_ha = new double[n];
	double* results_sin_la = new double[n];
	double* results_sin_ep = new double[n];
	double* results_cos_la = new double[n];
	double* results_cos_ha = new double[n];
	double* results_cos_ep = new double[n];

	// TODO: find out why calls are failing and add proper code here
	switch (function_code) {
	case VMf::Sin:
		//vmdSin(n, points, results_sin_ha, VML_HA);
		//vmdSin(n, points, results_sin_la, VML_LA);
		//vmdSin(n, points, results_sin_ep, VML_EP);
		break;
	case VMf::Cos:
		//vmdCos(n, points, results_cos_ha, VML_HA);
		//vmdCos(n, points, results_cos_la, VML_LA);
		//vmdCos(n, points, results_cos_ep, VML_EP);
		break;
	case VMf::SinCos:
		//vmdSinCos(n, points, results_sin_ha, results_cos_ha, VML_HA);
		//vmdSinCos(n, points, results_sin_la, results_cos_la, VML_LA);
		//vmdSinCos(n, points, results_sin_ep, results_cos_ep, VML_EP);
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
	
	delete[] results_sin_ha;
	delete[] results_sin_la;
	delete[] results_sin_ep;
	delete[] results_cos_ha;
	delete[] results_cos_la;
	delete[] results_cos_ep;
}