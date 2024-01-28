function onload(){
    if (bPracticeProj){
        init();
    }else{
        alert("TopMenu.js : Init : 起動失敗");
    }
}

async function init(){
    var sType="normal"; //画面タイプ
    var iSizeX=600;
    var iSizeY=400;
    //レイアウト更新要求
    await chrome.webview.hostObjects.ReqRx.ReqUpdLayout(sType,iSizeX,iSizeY);
    var sContName = "TopMenu"; //コンテンツ名
    //データ初期化要求
    await chrome.webview.hostObjects.ReqRx.ReqInitData(sContName);
    //モデル取得要求
    var modelB = await chrome.webview.hostObjects.ReqRx.ReqGetMdl(sContName);

    //JSON文字列配列を連想配列オブジェクトに変換
    var model = new Array();
    for(i=0;i<modelB.length;i++){
        model.push( ( new Function("return" + modelB[i]) )() );
    }

    //html要素の更新
    sPathCont1 = model[0].PATH;
    sPathCont2 = model[1].PATH;
    sPathCont3 = model[2].PATH;
    document.getElementById("btn-1").textContent = model[0].CONT;
    document.getElementById("btn-2").textContent = model[1].CONT;
    document.getElementById("btn-3").textContent = model[2].CONT;
}
