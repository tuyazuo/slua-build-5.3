cd ./

mkdir publish\android\armeabi-v7a
mkdir publish\android\x86

cd android

rem ndk-build clean APP_ABI="armeabi-v7a,x86"
ndk-build APP_ABI="armeabi-v7a,x86" APP_PLATFORM="18"

copy libs/armeabi-v7a/libslua.so ../publish/android/armeabi-v7a/libslua.so /y

echo build over----------------------------

pause
