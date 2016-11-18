mkdir -p publish/ios

cd ios

xcodebuild clean

xcodebuild -configuration=Release

cp -r build/Release-iphoneos/libslua.a ../publish/ios/