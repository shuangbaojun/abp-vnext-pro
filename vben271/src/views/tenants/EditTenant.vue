<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
  >
    <BasicForm @register="registerApiScopeForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { editFormSchema, updateTenantAsync } from './Tenant';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'EditTenant',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerApiScopeForm, { getFieldsValue, validate, resetFields, setFieldsValue }] =
        useForm({
          labelWidth: 120,
          schemas: editFormSchema,
          showActionButtonGroup: false,
        });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        setFieldsValue({
          name: data.record.name,
          id: data.record.id,
        });
      });

      const submit = async () => {
        try {
          const request = getFieldsValue();
          await updateTenantAsync({ request, changeOkLoading, validate, closeModal });
          resetFields();
          emit('reload');
        } finally {
          changeOkLoading(false);
        }
      };

      const cancel = () => {
        resetFields();
        closeModal();
      };
      return {
        t,
        registerModal,
        registerApiScopeForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>
