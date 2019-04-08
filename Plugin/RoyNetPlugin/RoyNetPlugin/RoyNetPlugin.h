#ifndef ROYNETPLUGIN_H
#define ROYNETPLUGIN_H

#define DLLExport __declspec(dllexport)

extern "C"
{
	DLLExport int testAdd(int a, int b);

	// start networking
	DLLExport int rnStart(int isServer);
	
	// update networking
	DLLExport int rnUpdate();

	// stop networking
	DLLExport int rnStop();
}

#endif // !RV_PHYS_H