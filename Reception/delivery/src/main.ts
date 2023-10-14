import { createApp } from "vue";
import App from "./App.vue";
// reset style sheet
import "@/styles/reset.scss";
// CSS common style sheet
import "@/styles/common.scss";
// iconfont css
import "@/assets/iconfont/iconfont.scss";
// font css
import "@/assets/fonts/font.scss";
// element css
import "element-plus/dist/index.css";
// element dark css
import "element-plus/theme-chalk/dark/css-vars.css";
// custom element dark css
import "@/styles/element-dark.scss";
// custom element css
import "@/styles/element.scss";
// svg icons
import "virtual:svg-icons-register";
// element plus
import ElementPlus from "element-plus";
// element icons
import * as Icons from "@element-plus/icons-vue";
// custom directives
import directives from "@/directives/index";
// vue Router
import router from "@/routers";
// vue i18n
import I18n from "@/languages/index";
// pinia store
import pinia from "@/stores";
// errorHandler
import errorHandler from "@/utils/errorHandler";
// 日期格式化
import dayjs from "dayjs";
// 过滤器
import * as filters from "@/filters/index"; // global filters

const app = createApp(App);
app.mixin({
  methods: {
    setTime(e: string | Date | number, type = "YYYY-MM-DD HH:mm") {
      return dayjs(e).format(type);
    }
  }
});

app.config.errorHandler = errorHandler;
app.config.globalProperties.$filters = filters;

// register the element Icons component
app.config.globalProperties.$icons = [];
Object.keys(Icons).forEach(key => {
  app.config.globalProperties.$icons.push(key);
  app.component(key, Icons[key as keyof typeof Icons]);
});

app.use(ElementPlus).use(directives).use(router).use(I18n).use(pinia).mount("#app");
