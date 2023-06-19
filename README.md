# Merge Bullet Clone
 
 resources klasöründe gamedata ve bulletdata olmak üzere 2 tane scriptable mevcut.
 game data üzerinde oyun dataların tutuluyor.toplam para miktarı değiştirilmek istenirse miktar değiştirip set data butonuna tıklamak yeterli. ammo price merge aşamasında aldığımız mermilerin fiyatını tutuyor.burda da değişiklik yapılabilir.
 mergegriddata ise merge aşamasındaki gridlerin bilgilerini tutuyor.oyuncu oyundan çıktığında merge aşamasındaki mermiler kaybolmuyor.
 bulletdata ise oyundaki mermileri tutuyor. oyuna mermi eklenmek isternirse gerekli yerler atanıp listeye eklenmesi yeterli.
 levelend highscore mantığı, oyuncu öldürdüğü kutulardan en uzakta olanı high score oluyor. gamedatada saklanıyor.
 shield objesinin üstündeki time değişkeni ne kadar koruyacağını belirliyor.
 
merge aşamasındaki duvarlar hepsi aynı canda oluşturuluyor. ek bir bilgi olmadığı için o şekilde yaptım.


gate objesini kalıtım alarak yaptım. biraz gereksiz gibi ama bilemedim :D
