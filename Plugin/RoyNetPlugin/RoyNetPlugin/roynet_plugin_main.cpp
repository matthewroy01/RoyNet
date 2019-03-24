/*
 *	RoyNet by Matthew Roy, 2019
 *	https://github.com/matthewroy01/RoyNet
 *
 *	good information on DLLs here: https://www.tutorialspoint.com/dll/dll_writing.htm
 *	original code by Dan Buckstein, 2018
*/

// Dan Buckstein code start

#if (defined _WINDOWS || defined _WIN32)

#include <Windows.h>

int APIENTRY DllMain(				// APIENTRY is an alias for WINAPI, a standard declaration for a Windows entrypoint
	HMODULE hModule,				// the base address of a DLL, can be used for targeting functions
	DWORD  ul_reason_for_call,		
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		// dispatched when another process (e.g. application) consumes this library
		break;
	case DLL_THREAD_ATTACH:
		// dispatched when another thread consumes this library
		break;
	case DLL_THREAD_DETACH:
		// dispatched when another thread releases this library
		break;
	case DLL_PROCESS_DETACH:
		// dispatched when another process releases this library
		break;
	}
	return TRUE;
}

#endif

// Dan Buckstein code end

int main()
{
	return 0;
}