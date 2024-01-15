function showPeriodOfWork(periodValue) {
    const days = periodValue.Days === 0 ? "" : `${periodValue.Days} дн.`;
    const hours = periodValue.Hours === 0 ? "" : `${periodValue.Hours} ч.`;
    const min = periodValue.Minutes === 0 ? "" : `${periodValue.Minutes} мин.`;
    const isAllEmpty = days === "" && hours === "" && min === "";
    const sec =
      periodValue.Seconds === 0
        ? `~ ${periodValue.Seconds} сек.`
        : `${periodValue.Seconds} сек.`;

    return `${days} ${hours} ${min} ${isAllEmpty ? sec : ""}`;
  }

export default showPeriodOfWork;
