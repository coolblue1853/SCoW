using System.Collections.Generic;
using SCoW.Core.State;

namespace SCoW.Core.Save
{
    /// <summary>한 버전을 올리는 마이그레이션. FromVersion → FromVersion+1.</summary>
    public interface ISaveMigration
    {
        int FromVersion { get; }
        void Apply(GameState state);
    }

    /// <summary>
    /// 저장 SchemaVersion 을 현재 버전까지 순서대로 올린다. 현재 버전보다 높으면 지원하지 않는다(NewerVersion).
    /// 출시 후 GameState 필드·키 의미를 바꿀 때 ISaveMigration 구현을 Default 에 추가한다(현재 1 → 마이그레이션 0개).
    /// </summary>
    public sealed class SaveMigrator
    {
        public static readonly SaveMigrator Default = new SaveMigrator(GameState.CurrentSchemaVersion, new ISaveMigration[0]);

        private readonly int _current;
        private readonly Dictionary<int, ISaveMigration> _byFrom = new Dictionary<int, ISaveMigration>();

        public SaveMigrator(int currentVersion, IEnumerable<ISaveMigration> migrations)
        {
            _current = currentVersion;
            foreach (ISaveMigration m in migrations)
            {
                _byFrom[m.FromVersion] = m;
            }
        }

        public int CurrentVersion
        {
            get { return _current; }
        }

        /// <summary>state.SchemaVersion 을 CurrentVersion 까지 올린다. 사슬이 끊겼으면 Corrupt.</summary>
        public SaveError Migrate(GameState state)
        {
            if (state.SchemaVersion > _current)
            {
                return SaveError.NewerVersion;
            }
            while (state.SchemaVersion < _current)
            {
                ISaveMigration m;
                if (!_byFrom.TryGetValue(state.SchemaVersion, out m))
                {
                    return SaveError.Corrupt;
                }
                m.Apply(state);
                state.SchemaVersion = m.FromVersion + 1;
            }
            return SaveError.None;
        }
    }
}
