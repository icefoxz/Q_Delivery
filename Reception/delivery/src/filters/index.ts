import dayjs from "dayjs";

// 获取时间戳
export function setTime(e: string | Date, type = "YYYY-MM-DD HH:mm") {
  return dayjs(e).format(type);
}
