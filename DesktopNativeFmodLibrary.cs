﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FmodForFoxes
{
	/// Windows and Linux-specific (and MacOS) part of an audio manager.
	/// See: https://github.com/Martenfur/FmodForFoxes/tree/develop/FmodForFoxes/FmodForFoxes.Desktop for the original source.
	public class DesktopNativeFmodLibrary : INativeFmodLibrary
	{
		private bool _loggingEnabled;

		public void Init(FmodInitMode mode, bool loggingEnabled = false)
		{
			_loggingEnabled = loggingEnabled;
			NativeLibrary.SetDllImportResolver(
				mode.GetType().Assembly, // Scoping the dllimport switcher for the crossplatform fmod lib.
				(libraryName, assembly, dllImportSearchPath) =>
				{
					libraryName = Path.GetFileNameWithoutExtension(libraryName);
					if (dllImportSearchPath == null)
					{
						dllImportSearchPath = DllImportSearchPath.AssemblyDirectory;
					}
					return NativeLibrary.Load(SelectDefaultLibraryName(libraryName, _loggingEnabled), assembly, dllImportSearchPath);
				}
			);
		}


		private string SelectDefaultLibraryName(string libName, bool loggingEnabled = false)
		{
			string name;

			if (OperatingSystem.IsWindows())
			{
				name = loggingEnabled ? $"{libName}L.dll" : $"{libName}.dll";
			}
			else if (OperatingSystem.IsLinux() || OperatingSystem.IsAndroid())
			{
				name = loggingEnabled ? $"lib{libName}L.so" : $"lib{libName}.so";
			}
			else if (OperatingSystem.IsMacOS())
			{
				name = loggingEnabled ? $"lib{libName}L.dylib" : $"lib{libName}.dylib";
			}
			else
			{
				throw new PlatformNotSupportedException();
			}

			return name;
		}
	}
}
