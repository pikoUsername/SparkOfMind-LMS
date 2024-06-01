using LMS.Domain.User.Enums;

namespace LMS.Domain.User.Services
{
    public static class PermissionService
    {

        private static bool CheckPermission(string permissionAcl, PermissionEnum action, object relation, Guid? relationId = null)
        {
            var parts = permissionAcl.Split(':');
            if (parts.Length != 3)
            {
                throw new Exception($"Permissions acl of the user has been compromised, parts: {parts}, acl: {permissionAcl}");
            }

            var subjectName = parts[0];
            var subjectId = parts[1];
            var subjectAction = parts[2];

            string? entityId; 
            if (relationId != null)
            {
                entityId = relationId.ToString();
            } else
            {
                entityId = GetEntityId(relation);
            }
            if (entityId == null)
            {
                throw new Exception("WTF, NO ENTITY ID!!"); 
            }

            // Проверка соответствия разрешений
            return (subjectId == entityId || subjectId == "*")
                && subjectName == relation.GetType().Name
                && subjectAction == action.ToString();
        }

        public static bool CheckPermissions(string[] permissionAcls, PermissionEnum action, object relation, Guid? relationId = null)
        {
            foreach (var permission in permissionAcls)
            {
                if (CheckPermission(permission, action, relation, relationId))
                {
                    return true;
                }
            }
            return false;
        }

        private static string GetEntityId(object relation)
        {
            if (relation == null)
            {
                throw new ArgumentNullException(nameof(relation), "Relation object cannot be null.");
            }

            if (relation is Type)
            {
                return relation.GetType().Name; 
            }

            if (relation is string stringRelation)
            {
                return stringRelation;
            }

            if (relation is BaseEntity entity)
            {
                return entity.Id.ToString();
            }

            throw new ArgumentException($"{nameof(relation)} entity is not convertible to BaseEntity", nameof(relation));
        }
    }
}
