function onload(){
    if (bPracticeProj){
        init();
    }else{
        alert("Bbb.js : Init : 起動失敗");
    }
}

async function init(){
    var sType="full"; //画面タイプ
    var iSizeX=800;
    var iSizeY=600;
    //レイアウト更新要求
    await chrome.webview.hostObjects.ReqRx.ReqUpdLayout(sType,iSizeX,iSizeY);
    var sContName = "Bbb"; //コンテンツ名
    //データ初期化要求
    await chrome.webview.hostObjects.ReqRx.ReqInitData(sContName);
    //モデル取得要求
    await chrome.webview.hostObjects.ReqRx.ReqGetMdl(sContName);

    var sCond = ""; //条件
    //結果取得要求
    await chrome.webview.hostObjects.ReqRx.ReqGetRslt(sContName,sCond);
}