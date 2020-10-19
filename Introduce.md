文件Electricity是项目文件，GameRelease为游戏发布文件，在release中含GameRelease与演示视频  
介绍项目文件Assets中的文件：  
  animations包含的动画帧文件和动画controller，其中包含两个人物动作动画，落地灰尘，终点，传送带，场景切换等动画文件   
  Mat原意为装materials，后来发现没用着   
  Music装音频文件，背景音乐与跳跃音效  
  Palette为tilemap的画笔文件，其中许多画笔并未用上（因为tilemap瓦片单独编辑如加脚本等操作不太熟练），只用tilemap铺了基本地形   
  Perfabs为预制体文件，其中Background为背景音乐预制体，belt为传送带，CM vcam是摄像机，DistanceControl是用来判断两人物是否连线，door为控制门，dust是落地灰尘
，electric filed为电场（给电场导体使得电场能够在两个玩家之间找到通路），eleM为可动继电器，eleS为静态继电器，end为终点，flash为连接两个物体的电，jump_sound如名
，LevelLoader起场景切换作用，plate为上下移动平台，PlayerF/M为两玩家，switch为连门开关，switch-trap为连陷阱开关，thorn为刺，trap为普通陷阱，trap-link为连开关陷阱
，ViewPoint为摄像机视点。   
  Scenes为场景文件  
  Scripts为脚本文件，AlwaysSound为背景音乐统一脚本，Beltmove为传送带移动，Destorysound为销毁产生的跳跃音效，Destorytrap与Destroytraplink控制陷阱销毁，Dieofthorn如名，
Dijkstra为迪杰斯特拉算法用于计算两玩家之间最短路（被定义为名空间，被电场调用），DonDestory用于防止背景音乐转场销毁，Dustdestroy销毁产生的落地灰，ElectricField用于电场（配合Dijkstra一起），
FlashLine用于产生电线（二者之间，这个被电场调用），PlateControl为传送带控制，PlayerDistanceDie为判断是否连线否则死亡变透明，PlayerF/Mcontrol为玩家控制，SceneEnter
场景进入时场景切换效果，scenesChange通关时场景切换，SwitchControl/SwitchtrapControl为开关控制，ViewPoint使得视点在二玩家之间  

  Sprite and Textures是原本图片资源  
