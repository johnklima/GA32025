// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"

#include <SDKDDKVer.h>
#include <Windows.h>

#include <stdio.h>
#include <conio.h>
#include <wchar.h>

#include <mmsystem.h>
#pragma comment(lib, "winmm.lib")



// The export mechanism used here is the __declspec(export)
// method supported by Microsoft Visual Studio, but any
// other export method supported by your development
// environment may be substituted.




#define EOF (-1)

int P1 = 0;
int P2 = 0;

UINT hello()
{
	return 256;
}

void PrintMidiDevices()
{
	UINT nMidiDeviceNum;
	MIDIINCAPS incaps;
	MIDIOUTCAPS outcaps;

	nMidiDeviceNum = midiInGetNumDevs();
	if (nMidiDeviceNum == 0) {
		fprintf(stderr, "midiInGetNumDevs() return 0...");
		return;
	}

	printf("MIDI input devices:\n");
	for (unsigned int i = 0; i < midiInGetNumDevs(); i++) {
		midiInGetDevCaps(i, &incaps, sizeof(MIDIINCAPS));
		wprintf(L"    %d : name = %s\n", i, incaps.szPname);
	}

	printf("MIDI output devices:\n");
	for (unsigned int i = 0; i < midiOutGetNumDevs(); i++) {
		midiOutGetDevCaps(i, &outcaps, sizeof(MIDIINCAPS));
		wprintf(L"    %d : name = %s\n", i, outcaps.szPname);
	}

	printf("\n");
}

void CALLBACK MidiInProc(HMIDIIN hMidiIn, UINT wMsg, DWORD dwInstance, DWORD dwParam1, DWORD dwParam2)
{
	switch (wMsg) {
	case MIM_OPEN:
		printf("wMsg=MIM_OPEN\n");
		break;
	case MIM_CLOSE:
		printf("wMsg=MIM_CLOSE\n");
		break;
	case MIM_DATA:
	{
		printf("wMsg=MIM_DATA, dwInstance=%08x, dwParam1=%08x, dwParam2=%08x\n", dwInstance, dwParam1, dwParam2);
		P1 =LOBYTE( HIWORD(dwParam1));
		P2 = HIBYTE(LOWORD(dwParam1));
		break;
	}
	case MIM_LONGDATA:
		printf("wMsg=MIM_LONGDATA\n");
		break;
	case MIM_ERROR:
		printf("wMsg=MIM_ERROR\n");
		break;
	case MIM_LONGERROR:
		printf("wMsg=MIM_LONGERROR\n");
		break;
	case MIM_MOREDATA:
		printf("wMsg=MIM_MOREDATA\n");
		break;
	default:
		printf("wMsg = unknown\n");
		break;
	}
	return;
}

int mymain()
{
	HMIDIIN hMidiDevice = NULL;;
	DWORD nMidiPort = 0;
	UINT nMidiDeviceNum;
	MMRESULT rv;

	PrintMidiDevices();

	nMidiDeviceNum = midiInGetNumDevs();
	if (nMidiDeviceNum == 0) {
		fprintf(stderr, "midiInGetNumDevs() return 0...");
		return -1;
	}

	rv = midiInOpen(&hMidiDevice, nMidiPort, (DWORD_PTR)(void*)MidiInProc, 0, CALLBACK_FUNCTION);
	if (rv != MMSYSERR_NOERROR) {
		fprintf(stderr, "midiInOpen() failed...rv=%d", rv);
		return -1;
	}

	

	return midiInStart(hMidiDevice);
}

#ifdef __cplusplus    // If used by C++ code, 
extern "C" {          // we need to export the C interface
#endif

	__declspec(dllexport) int __cdecl MIDI_Start()
	{
		
		return mymain();
	}

#ifdef __cplusplus
}
#endif

#ifdef __cplusplus    // If used by C++ code, 
extern "C" {          // we need to export the C interface
#endif

	__declspec(dllexport) int __cdecl getNote()
	{


		return P2;
	}

#ifdef __cplusplus
}
#endif

#ifdef __cplusplus    // If used by C++ code, 
extern "C" {          // we need to export the C interface
#endif

	__declspec(dllexport) int __cdecl getVelo()
	{


		return P1;
	}

#ifdef __cplusplus
}
#endif

