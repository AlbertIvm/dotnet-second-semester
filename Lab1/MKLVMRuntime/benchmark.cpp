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
	using mcs = std::chrono::duration<double, std::micro>;
	clock::time_point before;
	mcs duration;

	try {
		switch (function_code) {
		case VMf::Sin:
			// A hack to warm up cache
			vmdSin(n, points, results_sin_ha, VML_HA);
			vmdSin(n, points, results_sin_la, VML_LA);
			vmdSin(n, points, results_sin_ep, VML_EP);

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
			// A hack to warm up cache
			vmdCos(n, points, results_cos_ha, VML_HA);
			vmdCos(n, points, results_cos_la, VML_LA);
			vmdCos(n, points, results_cos_ep, VML_EP);

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
			// A hack to warm up cache
			vmdSin(n, points, results_sin_ha, VML_HA);
			vmdSin(n, points, results_sin_la, VML_LA);
			vmdSin(n, points, results_sin_ep, VML_EP);
			vmdCos(n, points, results_cos_ha, VML_HA);
			vmdCos(n, points, results_cos_la, VML_LA);
			vmdCos(n, points, results_cos_ep, VML_EP);

			// Run high accuracy calculations
			before = clock::now();
			vmdSinCos(n, points, results_sin_ha, results_cos_ha, VML_HA);
			duration = clock::now() - before;
			timings[0] = duration.count();

			// Run low accuracy calculations
			before = clock::now();
			vmdSinCos(n, points, results_sin_la, results_cos_la, VML_LA);
			duration = clock::now() - before;
			timings[1] = duration.count();

			// Run enhanced performance calculations
			before = clock::now();
			vmdSinCos(n, points, results_sin_ep, results_cos_ep, VML_EP);
			duration = clock::now() - before;
			timings[2] = duration.count();

			// Calculating errors, taking the maximum out of cos's and sin's errors
			for (size_t i = 0; i < n; ++i) {
				auto error_sin = abs(results_sin_ha[i] - results_sin_ep[i]);
				auto error_cos = abs(results_cos_ha[i] - results_cos_ep[i]);
				if (error_cos > error_sin and error_cos > max_error) {
					max_error = error_cos;
					max_error_arg = points[i];
					max_error_func_values[0] = results_cos_ha[i];
					max_error_func_values[1] = results_cos_la[i];
					max_error_func_values[2] = results_cos_ep[i];
				}
				if (error_sin > error_cos and error_sin > max_error) {
					max_error = error_sin;
					max_error_arg = points[i];
					max_error_func_values[0] = results_sin_ha[i];
					max_error_func_values[1] = results_sin_la[i];
					max_error_func_values[2] = results_sin_ep[i];
				}
			}
			break;
		}
	}
	catch (const std::exception& error) {
		// We quit quietly
		ret = -1;
		delete[] results_sin_ha;
		delete[] results_sin_la;
		delete[] results_sin_ep;
		delete[] results_cos_ha;
		delete[] results_cos_la;
		delete[] results_cos_ep;
		return;
	}
	
	delete[] results_sin_ha;
	delete[] results_sin_la;
	delete[] results_sin_ep;
	delete[] results_cos_ha;
	delete[] results_cos_la;
	delete[] results_cos_ep;

	// If everything went well
	ret = 0;
}