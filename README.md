# A Shadow Cast On Water
# 해당 프로젝트는 스팀 판매가 진행되고 있어 구 버전 빌드로 연결되어 있습니다. 최신화된 버전이 아닙니다.
<br/>


![스크린샷(27)](https://github.com/JNU-econovation/genom/assets/68218063/de151300-af5a-4d8a-abba-3e08786e2382)
 
스팀 출시를 위한 팀 프로젝트

<br/><br/>
    
[주제]  
TRPG의 규칙 중 하나인 Call of Cthulhu를 기반으로 키워드 중심의 추리 게임 제작 프로젝트
 <br/>
 
![image](https://github.com/JNU-econovation/genom/assets/68218063/acaa1d49-7b02-4e90-b934-c12e44077525)






<br/><br/>
[기술 스택]  

Unity3D / C# / Git


  


<br/><br/>
[개발 기능]  

대화창 및 키워드 시스템 :

<details>
 <br/>
 
 ![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/d6fc3c41-bdc9-4b07-98a0-901d0cd558b3)
<br/><br/>
csc파일 파싱을 이용해서 대화를 출력하는 프로그램을 작성하였습니다. 동작 방식은 아래와 같습니다
<br/><br/>
![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/40a3d3d7-cfd1-4384-94e9-ae5192e4ef15)

<br/>
<br/>


![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/072e3a12-f37d-4708-aa24-b2c0d03e52e2)
<br/><br/>
특수한 문자열이 사용되면 Dialog Manager 에서 연출에 관련된 함수가 출력됩니다.


그 외에도 스킵 기능, 선택지 출력 기능 등을 포함합니다.
<br/><br/>
![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/486c91dc-a79a-4998-9b63-9c3c0193873d)

<br/>

또한 대화중에 얻은 키워드들을 지정하여 대화 선택지의 Keyword를 E 버튼으로 활성화 시키면 해당 Key에 맞는 정보를 추가적으로 얻을 수 있습니다.

</details>
<br/>

캐릭터 생성:

<details>
여타 추리 게임과의 차이점으로 캐릭터 시트를 생성하여 랜덤 스탯을 배정 받고 각각 힘 / 지능 / 민첩의 수치만큼 기술을 부여할 수 있습니다.

DatabaseManager의 경우 싱글톤 인스턴스화를 통해 다른 스크립트에서도 고정된 능력치 변수를 참조 할 수 있도록 하였습니다.

![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/a88590bb-de3b-474d-8161-982f6927bb34)
<br/><br/>



</details>
<br/>

<br/>

주사위 판정 시스템:

<details>
 TRPG에서 가져온 시스템으로 내가 가지고 있는 기술 수치보다 더 낮은 수치가 룰렛을 통해 나오게 되면 판정에 성공 그렇지 못하면 실패하게 됩니다.
 각각의 결과에 따라 다른 대화, 보상이 나올수 있습니다.

 ![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/82856fc7-3456-4b80-b161-4f0ecd650c13)

</details>
<br/><br/>



UI(정보창, 상태창) :

<details>
  
 ![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/9eadb9d8-b633-4ec1-b3b2-1e1829b69337)
<br/><br/>
하단 UI의 경우에는 가장 좌측에 상호작용 하는 대상에 대한 정보를 볼수있는 Detail 창과 중앙의 체력 / 이성 창, 그리고 키워드를 지정할 수 있는 창으로 구분되어있습니다.

![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/035b4d8c-7a19-4da8-9e23-15b4d8f5b643) ![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/50f9a6c9-367a-4eb7-a083-a4ebb1a00b77)
<br/><br/>
상태창의 경우에는 현재 능력치와 디버프, 보유 기술 수치 뿐만 아니라 인벤토리 또한 포함하고 있습니다.
인벤토리는 장착 / 사용 / 키 아이템으로 구분됩니다.


![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/6ff230b5-d62c-47b1-9f03-b0ea235c0d6f)
<br/><br/>
정보창의 경우 대화를 통해 얻는 키워드의 상세 정보를 다시 확인할 수 있으며 원하는 키워드를 SetKeyword 버튼을 통해 선택할 수 있습니다.


</details>
<br/>

그래픽 후처리 :

<details>

2D에서 그래픽 리소스를 많이 차지할 수 있는 물반사, 광원, 시간 흐름 등을 URP를 이용한 후처리로  보완했습니다.

물 반사의 경우
<br/>

먼저 위와 반사되어 보일 장면을 별도의 카메라를 통해 촬영합니다.

그리고 Shader Graph를 이용하여 텍스쳐와 시간에 따라 우측으로 이동하는 효과를 병합하여 결과로 출력하면 아래와 같은 물 반사 효과를 얻을 수 있었습니다.
<br/><br/>
![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/365a0fb7-d209-4979-8264-cebcc44cdad0)



 

</details>
<br/>

턴제 전투 시스템 :

<details>

 ![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/c8fa35de-3d57-4b5e-973f-c921259f9d67)
<br/><br/>
 앞서 만들어진 판정 시스템을 기반으로 하여 나와 적의 판정을 비교하여 더 우세한 판정을 받은 결과값이 발현되도록 하는 시스템입니다
 판정은 각각 대성공 / 성공 / 실패 / 대실패로 나뉩니다.

<br/>

 FSM을 통해서 전투를 구성하였고 구조는 아래와 같습니다.

 ![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/35b9bd13-783e-451f-8886-5d47339df960)
<br/><br/>
State 흐름도는 아래와 같습니다
![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/04ec0be0-d86e-497d-8543-370817c022a2)


</details>
<br/>

이성(디버프) 시스템:
<details>

 ![image](https://github.com/coolblue1853/PlamTurtle_Coc/assets/54536107/7ad0a229-39ce-4128-926b-8b953326a37a)
<br/><br/>
 
 플레이어가 '이성' 이라는 특정 판정에 실패 하였으면 발동되는 시스템으로 각각 이성감소 / 광기 발현 / 컨디션 감소 중 하나를 발동 시킵니다.

 그중 광기 발현이 디버프 시스템으로  총 3단계로 구성되어있으며 세부 내용은 아래와 같습니다.
 
 1단계 : 게임에 지장이 없는, 혹은 단순 능력치 감소
 
 2단계 : 게임에 지장을 줄수 있는, 큰 폭의 능력치 감소
 
 3단계 : 게임에 심각한 지장을 주며, 시각적으로 불편한


</details>
<br/>

