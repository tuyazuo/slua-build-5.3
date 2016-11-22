lua5.3.3的vm编译，完全采用源码编译方式
windows编译文件 build_win32.cmd build_win64.cmd 需要在里面配置mingw路径
android编译文件 build_android.sh 使用32位NDK编译，切记用32位NDK，当下还有很多android手机不支持64位
mac编译文件 build_mac.sh
ios编译文件 build_ios.sh
其余文件为未完善状态，慎用
编译中遇到些问题，在下面的博客中都找到解决方法了，非常感谢博主
http://www.cnblogs.com/yaukey/p/slua-530-compile-project.html
https://github.com/yaukeywang/slua-503-build
