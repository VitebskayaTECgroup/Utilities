function checkIsNeedLoadDataOnScroll(e, funcForLoadData) {
    const COUNT_SCREEN_HEIGHT_LOGS = 1;
    const target = e.target;
    const heightLogs = target.scrollHeight; // Высота всего окна logs, включая прокрутку
    const screenHeightLogs = target.getBoundingClientRect().height; //Высота окна logs, которую мы видим
    const scrolledLogs = target.scrollTop; // Где находится окно logs, которое видно на странице, относительно всей своей высоты
    const threshold = heightLogs - screenHeightLogs * COUNT_SCREEN_HEIGHT_LOGS;
    const position = scrolledLogs + screenHeightLogs;

    if (position >= threshold)  funcForLoadData()
}

export default checkIsNeedLoadDataOnScroll;
