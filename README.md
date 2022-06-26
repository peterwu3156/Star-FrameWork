# Star FrameWork

基于唐老狮的Unity基础框架更新迭代的版本

感兴趣的同学可以学习<https://www.taikr.com/course/1062>

目前正在测试优化和debug



#### 增加的功能

###### 对象池能够进行冗余回收 

当某一个时间段创建了大量的对象之后不再使用那么多了

不回收就会占用一些空间不动 所以定时批量回收到允许的最大值

###### 音效使用对象池

老师在这里是单个物体挂在多个AudioSource脚本完成的

考虑到音效实际的数量会比较多 在老师的帮助下改写成了对象池的版本

###### UI基类的一些扩展

比较冷门的组件 例如ToggleGroup



#### 计划拓展的功能

其它可以封装的系统 例如任务系统
