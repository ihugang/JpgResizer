# JpgResizer

# 客户不懂技术，只管提需求

网站在建立的时候就已经告知会有什么优点和弱项了，但是客户都是善于遗忘的人。他们只管现在，不管过去。所以唯一不必的是变化这句话就又被验证了一次。
怎么办呢？
 1. 全新的界面设计 ，重构网站？显然不太现实，刚做好没两个月。
 2. 缩小图片文件大小是唯一可行可试的方案。第一步缩小时将png全部改为了jpg，因为不用考虑透明度的问题。第二步就是降低jpg图片的质量，随之减小文件大小。
 在[tinypng.com](https://www.tinypng.com) 做了几张，感觉太麻烦了，这要做到几时去呢？
 于是自己写了一个小工具，可以批量的缩小jpg文件的大小，当然图片质量会略有下降了，但是普通访问者都几乎无法注意到，是可以接受的。
 
为了方便，写了一个Console应用，命令行方式运行：
Please enter resize argument.

Usage: JpgResizer <filename> -r 75 -o -b

Usage: JpgResizer *.jpg -r 75 -o -b -s

 -b  : 备份原始文件，在覆盖模式时有用 backup the original file. use when -o is set.
 
 -o  : 覆盖模式，直接重写原文件 overwrite the original file.
 
 -s  : 搜索当前目录下的所有子目录下的文件 search all jpg files in sub-directories. ignore filename.
 
 -r  : 压缩后图片质量级别 compress level. default is 75, 100 is best.
 
 
 *在杭州的创业程序员*
 如果您有小工具方面的需求，可以联系，在以下范围都可以。
 1、Html5 javascript 小游戏。 案例：https://render.mybank.cn/p/w/bkminishop/ 
 2、iOS app开发。 AppStore上搜索“引力网络”
 3、C# winform 工具，文档处理等效率工具定制开发。
 联系：QQ390652
