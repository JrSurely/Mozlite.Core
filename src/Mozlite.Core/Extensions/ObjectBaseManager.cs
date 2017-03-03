using System.Threading.Tasks;
using Mozlite.Data;

namespace Mozlite.Extensions
{
    /// <summary>
    /// �������ʵ�ֻ��ࡣ
    /// </summary>
    /// <typeparam name="TModel">ʵ�����͡�</typeparam>
    public abstract class ObjectBaseManager<TModel> : IObjectBaseManager<TModel> where TModel : IObjectBase
    {
        /// <summary>
        /// ���ݿ�����ӿڡ�
        /// </summary>
        protected readonly IRepository<TModel> Database;
        /// <summary>
        /// ��ʼ����<see cref="ObjectManager{TModel}"/>��
        /// </summary>
        /// <param name="repository">���ݿ�����ӿڡ�</param>
        protected ObjectBaseManager(IRepository<TModel> repository)
        {
            Database = repository;
        }

        /// <summary>
        /// �ж�ʵ���Ƿ��ظ���
        /// </summary>
        /// <param name="model">ģ��ʵ����</param>
        /// <returns>�����жϽ����</returns>
        public virtual bool IsDulicate(TModel model)
        {
            return false;
        }

        /// <summary>
        /// �ж�ʵ���Ƿ��ظ���
        /// </summary>
        /// <param name="model">ģ��ʵ����</param>
        /// <returns>�����жϽ����</returns>
        public virtual Task<bool> IsDulicateAsync(TModel model)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// ����ʵ����
        /// </summary>
        /// <param name="model">ģ��ʵ������</param>
        /// <returns>����ִ�н����</returns>
        public virtual DataResult Save(TModel model)
        {
            if (IsDulicate(model))
                return DataAction.Duplicate;
            if (model.Id > 0)
                return DataResult.FromResult(Database.Update(model), DataAction.Updated);
            return DataResult.FromResult(Database.Create(model), DataAction.Created);
        }

        /// <summary>
        /// ����ʵ����
        /// </summary>
        /// <param name="model">ģ��ʵ������</param>
        /// <returns>����ִ�н����</returns>
        public virtual async Task<DataResult> SaveAsync(TModel model)
        {
            if (await IsDulicateAsync(model))
                return DataAction.Duplicate;
            if (model.Id > 0)
                return DataResult.FromResult(await Database.UpdateAsync(model), DataAction.Updated);
            return DataResult.FromResult(await Database.CreateAsync(model), DataAction.Created);
        }

        /// <summary>
        /// ��ҳ�����ĵ��б�
        /// </summary>
        /// <param name="query">�ĵ��б��ѯʵ����</param>
        /// <returns>�����ĵ��б�</returns>
        public virtual TQuery Load<TQuery>(TQuery query) where TQuery : QueryBase<TModel>
        {
            return Database.Load(query);
        }

        /// <summary>
        /// ��ҳ�����ĵ��б�
        /// </summary>
        /// <param name="query">�ĵ��б��ѯʵ����</param>
        /// <returns>�����ĵ��б�</returns>
        public virtual Task<TQuery> LoadAsync<TQuery>(TQuery query) where TQuery : QueryBase<TModel>
        {
            return Database.LoadAsync(query);
        }

        /// <summary>
        /// ��ȡģ��ʵ����
        /// </summary>
        /// <param name="id">ʵ��Id��</param>
        /// <returns>����ģ��ʵ������</returns>
        public virtual TModel Find(int id)
        {
            return Database.Find(x => x.Id == id);
        }

        /// <summary>
        /// ��ȡģ��ʵ����
        /// </summary>
        /// <param name="id">ʵ��Id��</param>
        /// <returns>����ģ��ʵ������</returns>
        public virtual async Task<TModel> FindAsync(int id)
        {
            return await Database.FindAsync(x => x.Id == id);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="id">Id��</param>
        /// <returns>����ɾ�������</returns>
        public virtual DataResult Delete(int id)
        {
            return DataResult.FromResult(Database.Delete(x => x.Id == id), DataAction.Deleted);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="ids">Id���ϡ�</param>
        /// <returns>����ɾ�������</returns>
        public virtual DataResult Delete(int[] ids)
        {
            return DataResult.FromResult(Database.Delete(x => x.Id.Included(ids)), DataAction.Deleted);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="ids">Id���ϣ��ԡ�,���ָ</param>
        /// <returns>����ɾ�������</returns>
        public virtual DataResult Delete(string ids)
        {
            return Delete(ids.SplitToInt32());
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="id">Id��</param>
        /// <returns>����ɾ�������</returns>
        public virtual async Task<DataResult> DeleteAsync(int id)
        {
            return DataResult.FromResult(await Database.DeleteAsync(x => x.Id == id), DataAction.Deleted);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="ids">Id���ϡ�</param>
        /// <returns>����ɾ�������</returns>
        public virtual async Task<DataResult> DeleteAsync(int[] ids)
        {
            return DataResult.FromResult(await Database.DeleteAsync(x => x.Id.Included(ids)), DataAction.Deleted);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="ids">Id���ϣ��ԡ�,���ָ</param>
        /// <returns>����ɾ�������</returns>
        public virtual Task<DataResult> DeleteAsync(string ids)
        {
            return DeleteAsync(ids.SplitToInt32());
        }

        /// <summary>
        /// ���ʵ����
        /// </summary>
        /// <param name="model">ģ��ʵ������</param>
        /// <returns>����ִ�н����</returns>
        public virtual DataResult Create(TModel model)
        {
            if (IsDulicate(model))
                return DataAction.Duplicate;
            return DataResult.FromResult(Database.Create(model), DataAction.Created);
        }

        /// <summary>
        /// ���ʵ����
        /// </summary>
        /// <param name="model">ģ��ʵ������</param>
        /// <returns>����ִ�н����</returns>
        public virtual async Task<DataResult> CreateAsync(TModel model)
        {
            if (await IsDulicateAsync(model))
                return DataAction.Duplicate;
            return DataResult.FromResult(await Database.CreateAsync(model), DataAction.Created);
        }

        /// <summary>
        /// ����ʵ����
        /// </summary>
        /// <param name="model">ģ��ʵ������</param>
        /// <returns>����ִ�н����</returns>
        public virtual DataResult Update(TModel model)
        {
            if (IsDulicate(model))
                return DataAction.Duplicate;
            return DataResult.FromResult(Database.Update(model), DataAction.Updated);
        }

        /// <summary>
        /// ����ʵ����
        /// </summary>
        /// <param name="model">ģ��ʵ������</param>
        /// <returns>����ִ�н����</returns>
        public virtual async Task<DataResult> UpdateAsync(TModel model)
        {
            if (await IsDulicateAsync(model))
                return DataAction.Duplicate;
            return DataResult.FromResult(await Database.UpdateAsync(model), DataAction.Updated);
        }
    }
}