function onload(){
    if (bPracticeProj){
        init();
    }else{
        alert("Aaa.js : Init : 起動失敗");
    }
}

async function init(){
    var sType="normal"; //画面タイプ
    var iSizeX=700;
    var iSizeY=500;
    //レイアウト更新要求
    await chrome.webview.hostObjects.ReqRx.ReqUpdLayout(sType,iSizeX,iSizeY);
    var sContName = "Aaa"; //コンテンツ名
    //データ初期化要求
    await chrome.webview.hostObjects.ReqRx.ReqInitData(sContName);
    //モデル取得要求
    var model = await chrome.webview.hostObjects.ReqRx.ReqGetMdl(sContName);
    
    console.log("Aaa.js : ReqGetMdl : 結果");
    console.log(model);

    var sCond = ""; //条件
    //結果取得要求
    await chrome.webview.hostObjects.ReqRx.ReqGetRslt(sContName,sCond);
}
