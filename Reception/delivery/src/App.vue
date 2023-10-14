<template>
  <el-config-provider :locale="locale" :size="assemblySize" :button="buttonConfig">
    <router-view></router-view>
  </el-config-provider>
  <!-- 终于可以提交了 -->
</template>

<script setup lang="ts">
import { onMounted, reactive, computed } from "vue";
import { useI18n } from "vue-i18n";
import { getBrowserLang } from "@/utils";
import { useTheme } from "@/hooks/useTheme";
import { ElConfigProvider } from "element-plus";
import { LanguageType } from "./stores/interface";
import { useGlobalStore } from "@/stores/modules/global";
import en from "element-plus/es/locale/lang/en";
import zhCn from "element-plus/es/locale/lang/zh-cn";

const globalStore = useGlobalStore();

// init theme
const { initTheme } = useTheme();
initTheme();

// init language
const i18n = useI18n();
onMounted(() => {
  const language = globalStore.language ?? getBrowserLang();
  i18n.locale.value = language;
  globalStore.setGlobalState("language", language as LanguageType);
});

// element language
const locale = computed(() => {
  if (globalStore.language == "zh") return zhCn;
  if (globalStore.language == "en") return en;
  return getBrowserLang() == "zh" ? zhCn : en;
});

// element assemblySize
const assemblySize = computed(() => globalStore.assemblySize);

// element button config
const buttonConfig = reactive({ autoInsertSpace: false });
</script>

<style lang="scss">
// 文字溢出隐藏
%line-clamp {
  display: -webkit-box;
  overflow: hidden;
  text-overflow: ellipsis;
  -webkit-box-orient: vertical;
}
.text-line-1 {
  @extend %line-clamp;

  -webkit-line-clamp: 1;
}
.text-line-2 {
  @extend %line-clamp;

  -webkit-line-clamp: 2;
}
.text-line-3 {
  @extend %line-clamp;

  -webkit-line-clamp: 3;
}
.text-line-4 {
  @extend %line-clamp;

  -webkit-line-clamp: 4;
}
.full-table .el-table {
  height: 55vh !important;

  // border: 1px solid red !important;
}
</style>
