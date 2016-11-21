
PATH=D:\UnityPackage\Lua\MinGW-master\x64\bin;%PATH%
mkdir publish\windows\x64

gcc	lua-src\slua\slua.c ^
	lua-src\lua-5.3.3\src\lapi.c ^
	lua-src\lua-5.3.3\src\lauxlib.c ^
	lua-src\lua-5.3.3\src\lbaselib.c ^
	lua-src\lua-5.3.3\src\lbitlib.c ^
	lua-src\lua-5.3.3\src\lcode.c ^
	lua-src\lua-5.3.3\src\lcorolib.c ^
	lua-src\lua-5.3.3\src\lctype.c ^
	lua-src\lua-5.3.3\src\ldblib.c ^
	lua-src\lua-5.3.3\src\ldebug.c ^
	lua-src\lua-5.3.3\src\ldo.c ^
	lua-src\lua-5.3.3\src\ldump.c ^
	lua-src\lua-5.3.3\src\lfunc.c ^
	lua-src\lua-5.3.3\src\lgc.c ^
	lua-src\lua-5.3.3\src\linit.c ^
	lua-src\lua-5.3.3\src\liolib.c ^
	lua-src\lua-5.3.3\src\llex.c ^
	lua-src\lua-5.3.3\src\lmathlib.c ^
	lua-src\lua-5.3.3\src\lmem.c ^
	lua-src\lua-5.3.3\src\loadlib.c ^
	lua-src\lua-5.3.3\src\lobject.c ^
	lua-src\lua-5.3.3\src\lopcodes.c ^
	lua-src\lua-5.3.3\src\loslib.c ^
	lua-src\lua-5.3.3\src\lparser.c ^
	lua-src\lua-5.3.3\src\lstate.c ^
	lua-src\lua-5.3.3\src\lstring.c ^
	lua-src\lua-5.3.3\src\lstrlib.c ^
	lua-src\lua-5.3.3\src\ltable.c ^
	lua-src\lua-5.3.3\src\ltablib.c ^
	lua-src\lua-5.3.3\src\ltm.c ^
	lua-src\lua-5.3.3\src\lundump.c ^
	lua-src\lua-5.3.3\src\lutf8lib.c ^
	lua-src\lua-5.3.3\src\lvm.c ^
	lua-src\lua-5.3.3\src\lzio.c ^
	-I./lua-src\lua-5.3.3\src ^
	lua-src\sproto-master\sproto.c ^
	lua-src\sproto-master\lsproto.c ^
	-I./lua-src\sproto-master ^
	lua-src\lua-cjson-master\fpconv.c ^
	lua-src\lua-cjson-master\lua_cjson.c ^
	lua-src\lua-cjson-master\strbuf.c ^
	-I./lua-src\lua-cjson-master ^
	lua-src\lpeg\lpcap.c ^
	lua-src\lpeg\lpcode.c ^
	lua-src\lpeg\lpprint.c ^
	lua-src\lpeg\lptree.c ^
	lua-src\lpeg\lpvm.c ^
	-I./lua-src\lpeg ^
	-o publish/windows/x64/slua.dll -m64 -O3 -std=gnu99 -shared
	
copy publish/windows/x64/slua.dll Unity/Assets/Plugins/x64/slua.dll
echo build over----------------------------
pause

	lua-5.3.3\src\lua.c ^
	lua-5.3.3\src\luac.c ^
