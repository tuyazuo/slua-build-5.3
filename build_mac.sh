mkdir -p publish/mac

cd macosx

xcodebuild clean

xcodebuild -configuration=Release

cp -r build/Release/slua.bundle ../publish/mac/