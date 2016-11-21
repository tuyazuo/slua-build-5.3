
mkdir -p publish/android/armeabi-v7a
mkdir -p publish/android/x86

cd android

ndk-build clean APP_ABI="armeabi-v7a,x86"
ndk-build APP_ABI="armeabi-v7a,x86" APP_PLATFORM="18"

cp libs/armeabi-v7a/libslua.so ../publish/android/armeabi-v7a/libslua.so
cp libs/x86/libslua.so ../publish/android/x86/libslua.so

echo build over----------------------------