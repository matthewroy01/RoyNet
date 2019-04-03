/*
 *	RoyNet by Matthew Roy, 2019
 *	https://github.com/matthewroy01/RoyNet
 *
 *	good information on DLLs here: https://www.tutorialspoint.com/dll/dll_writing.htm
 *	original code by Dan Buckstein, 2018
*/

#ifndef ROYNET_PLUGIN_H
#define ROYNET_PLUGIN_H

// Dan Buckstein code start

#include "roynet_plugin_config.h"
#ifdef __cplusplus

extern "C"
{
#endif // __cplusplus

	ROYNET_SYMBOL
	int TestFunction();

#ifdef __cplusplus
}
#endif // __cplusplus

// Dan Buckstein code end

#endif	// !ROYNET_PLUGIN_H