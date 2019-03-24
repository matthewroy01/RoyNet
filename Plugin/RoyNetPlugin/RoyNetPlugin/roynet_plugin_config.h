/*
 *	RoyNet by Matthew Roy, 2019
 *	https://github.com/matthewroy01/RoyNet
 *
 *	good information on DLLs here: https://www.tutorialspoint.com/dll/dll_writing.htm
 *	original code by Dan Buckstein, 2018
*/

#ifndef ROYNET_PLUGIN_CONFIG_H
#define ROYNET_PLUGIN_CONFIG_H

// Dan Buckstein code start

#ifdef ROYNET_DLLEXPORT

#define ROYNET_SYMBOL __declspec(dllexport)

#else // !EXPORT
	#ifdef ROYNET_DLLIMPORT

	#define ROYNET_SYMBOL __declspec(dllimport)

	#else // !IMPORT
		#define ROYNET_SYMBOL

	#endif // IMPORT
#endif // EXPORT

// Dan Buckstein code end

#endif // !ROYNET_PLUGIN_CONFIG_H