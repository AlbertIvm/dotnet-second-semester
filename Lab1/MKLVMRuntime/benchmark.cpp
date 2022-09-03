#include "pch.h"

#include <chrono>
#include <cmath>
#include <iostream>

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
	// These will only be used internally
	double* results_sin_ha = new double[n];
	double* results_sin_la = new double[n];
	double* results_sin_ep = new double[n];
	double* results_cos_la = new double[n];
	double* results_cos_ha = new double[n];
	double* results_cos_ep = new double[n];

	// A hack to avoid code copying for initialization of parameters
	max_error = -1;

	// Time measurement code is far from perfect, but is best
	// I could do without a higher order function
	using clock = std::chrono::system_clock;
	using ms = std::chrono::duration<double, std::milli>;
	clock::time_point before;
	ms duration;

	// A hack to warm up cache
	vmdSin(n, points, results_sin_ha, VML_EP);
	
	switch (function_code) {
	case VMf::Sin:
		// Run high accuracy calculations
		before = clock::now();
		vmdSin(n, points, results_sin_ha, VML_HA);
		duration = clock::now() - before;
		timings[0] = duration.count();
		
		// Run low accuracy calculations
		before = clock::now();
		vmdSin(n, points, results_sin_la, VML_LA);
		duration = clock::now() - before;
		timings[1] = duration.count();

		// Run enhanced performance calculations
		before = clock::now();
		vmdSin(n, points, results_sin_ep, VML_EP);
		duration = clock::now() - before;
		timings[2] = duration.count();

		// Calculating errors
		for (size_t i = 0; i < n; ++i) {
			auto error = abs(results_sin_ha[i] - results_sin_ep[i]);
			if (error > max_error) {
				max_error = error;
				max_error_arg = points[i];
				max_error_func_values[0] = results_sin_ha[i];
				max_error_func_values[1] = results_sin_la[i];
				max_error_func_values[2] = results_sin_ep[i];
			}
		}
		break;
	case VMf::Cos:
		// Run high accuracy calculations
		before = clock::now();
		vmdCos(n, points, results_cos_ha, VML_HA);
		duration = clock::now() - before;
		timings[0] = duration.count();

		// Run low accuracy calculations
		before = clock::now();
		vmdCos(n, points, results_cos_la, VML_LA);
		duration = clock::now() - before;
		timings[1] = duration.count();

		// Run enhanced performance calculations
		before = clock::now();
		vmdCos(n, points, results_cos_ep, VML_EP);
		duration = clock::now() - before;
		timings[2] = duration.count();

		// Calculating errors
		for (size_t i = 0; i < n; ++i) {
			auto error = abs(results_cos_ha[i] - results_cos_ep[i]);
			if (error > max_error) {
				max_error = error;
				max_error_arg = points[i];
				max_error_func_values[0] = results_cos_ha[i];
				max_error_func_values[1] = results_cos_la[i];
				max_error_func_values[2] = results_cos_ep[i];
			}
		}
		break;
	case VMf::SinCos:
		//vmdSinCos(n, points, results_sin_ha, results_cos_ha, VML_HA);
		//vmdSinCos(n, points, results_sin_la, results_cos_la, VML_LA);
		//vmdSinCos(n, points, results_sin_ep, results_cos_ep, VML_EP);
		break;
	}

	// TODO: Dummy code, rewrite later
	ret = 0;
	
	delete[] results_sin_ha;
	delete[] results_sin_la;
	delete[] results_sin_ep;
	delete[] results_cos_ha;
	delete[] results_cos_la;
	delete[] results_cos_ep;
}