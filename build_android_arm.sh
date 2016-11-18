
mkdir -p publish\android\armeabi-v7a

cd android

ndk-build clean APP_ABI="armeabi-v7a"
ndk-build APP_ABI="armeabi-v7a"

cp libs/armeabi-v7a/libslua.so ../publish/android/armeabi-v7a/libslua.so

echo build over----------------------------

pause
