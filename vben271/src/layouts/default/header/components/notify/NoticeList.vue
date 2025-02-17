<template>
  <a-list :class="prefixCls" bordered>
    <template v-for="item in list" :key="item.id">
      <a-list-item class="list-item">
        <a-list-item-meta>
          <template #title>
            <div class="title">
              <a-typography-paragraph
                style="width: 100%; margin-bottom: 0 !important"
                :ellipsis="titleRows > 0 ? { rows: titleRows, tooltip: item.title } : false"
                :content="item.title"
              />
              <div>
                <a-tag class="tag" :color="item.read ? 'green' : 'red'">
                  {{ item.read ? '已读' : '未读' }}
                </a-tag>
                <a-tag v-if="!item.read" class="tag" color="green" @click="onSetRead(item)">
                  {{ '标记为已读' }}
                </a-tag>
              </div>
            </div>
          </template>

          <template #description>
            <div>
              <div class="description">
                <a-typography-paragraph
                  style="width: 100%; margin-bottom: 0 !important"
                  :ellipsis="descRows > 0 ? { rows: descRows, tooltip: item.content } : false"
                  :content="item.content"
                />
              </div>
              <div class="datetime">
                {{ dateFormat(item.creationTime) }}
              </div>
            </div>
          </template>
        </a-list-item-meta>
      </a-list-item>
    </template>
  </a-list>
</template>
<script lang="ts">
  import { defineComponent, PropType } from 'vue';
  import { setReadAsync } from './data';
  import { useDesign } from '/@/hooks/web/useDesign';
  import { List, Tag, Typography } from 'ant-design-vue';
  import { PagingNotificationListOutput, SetReadInput } from '/@/services/ServiceProxies';
  import { useUserStoreWithOut } from '/@/store/modules/user';
  export default defineComponent({
    components: {
      [List.name]: List,
      [List.Item.name]: List.Item,
      AListItemMeta: List.Item.Meta,
      ATypographyParagraph: Typography.Paragraph,
      [Tag.name]: Tag,
    },
    props: {
      list: {
        type: Array as PropType<PagingNotificationListOutput[]>,
        default: () => [],
      },
      titleRows: {
        type: Number,
        default: 1,
      },
      descRows: {
        type: Number,
        default: 1,
      },
    },
    setup() {
      const userStore = useUserStoreWithOut();
      const userInfo = userStore.getUserInfo;

      const onSetRead = async (record: PagingNotificationListOutput) => {
        let request = new SetReadInput();
        request.id = record.id;
        request.receiveId = userInfo.userId as string;
        await setReadAsync(request);
        record.read = true;
        console.log(record);
      };
      const dateFormat = (time) => {
        let date = new Date(time);
        let year = date.getFullYear();
        let month = date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1;
        let day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
        let hours = date.getHours() < 10 ? '0' + date.getHours() : date.getHours();
        let minutes = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes();
        let seconds = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds();
        // 拼接
        return year + '-' + month + '-' + day + ' ' + hours + ':' + minutes + ':' + seconds;
      };
      const { prefixCls } = useDesign('header-notify-list');
      return { prefixCls, dateFormat, onSetRead };
    },
  });
</script>
<style lang="less" scoped>
  @prefix-cls: ~'@{namespace}-header-notify-list';

  .@{prefix-cls} {
    &::-webkit-scrollbar {
      display: none;
    }

    ::v-deep(.ant-pagination-disabled) {
      display: inline-block !important;
    }

    &-item {
      padding: 6px;
      overflow: hidden;
      cursor: pointer;
      transition: all 0.3s;

      .title {
        margin-bottom: 8px;
        font-weight: normal;

        .extra {
          float: right;
          margin-top: -1.5px;
          margin-right: 0;
          font-weight: normal;

          .tag {
            font-weight: normal;
          }
        }
        .avatar {
          margin-top: 4px;
        }

        .description {
          font-size: 12px;
          line-height: 18px;
        }

        .datetime {
          margin-top: 4px;
          font-size: 12px;
          line-height: 18px;
        }
      }
    }
  }
</style>
