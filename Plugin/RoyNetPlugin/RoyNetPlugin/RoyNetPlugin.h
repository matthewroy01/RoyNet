#ifndef ROYNETPLUGIN_H
#define ROYNETPLUGIN_H

#define DLLExport __declspec(dllexport)

struct Msg_TestTransform;

extern "C"
{
	DLLExport int testAdd(int a, int b);

	DLLExport int sendTestTransform(Msg_TestTransform msg);

	// start networking
	DLLExport int rnStart(int isServer);
	
	// update networking
	DLLExport int rnUpdate();

	// stop networking
	DLLExport int rnStop();
}

#endif // !RV_PHYS_H