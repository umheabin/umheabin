using UnityEngine;
using UnityEngine.UI;

namespace BulletPoint
{
    // ▶ 이 오브젝트에는 반드시 VerticalLayoutGroup이 있어야 함
    //    없으면 Unity가 자동으로 추가해 줌
    [RequireComponent(typeof(VerticalLayoutGroup))]

    // ▶ 이 오브젝트에는 반드시 ContentSizeFitter가 있어야 함
    //    없으면 Unity가 자동으로 추가해 줌
    [RequireComponent(typeof(ContentSizeFitter))]
    public class BulletPoint : MonoBehaviour
    {
        #region Inspector Variables

        // ▶ Inspector에 "Text Style"이라는 제목 표시
        [Header("Text Style")]

        // ▶ 생성되는 모든 BulletPointItem의 기본 글자 색상
        public Color textColor = Color.black;

        // ▶ 생성되는 모든 BulletPointItem의 기본 글자 크기
        public float textSize = 24;

        // ▶ BulletPointItem 프리팹
        //    Inspector에서 직접 연결
        [Header("Prefab")]
        public BulletPointItem itemPrefab;

        #endregion

        #region Private Variables

        // ▶ 세로 정렬을 담당하는 UI 컴포넌트
        private VerticalLayoutGroup verticalGroup;

        // ▶ 내용 크기에 따라 RectTransform 크기를 자동 조절
        private ContentSizeFitter contentSizeFitter;

        #endregion

        #region Unity Methods

        // ▶ 오브젝트가 생성될 때 한 번 호출
        private void Awake()
        {
            // 현재 오브젝트에 붙어 있는 컴포넌트 가져오기
            verticalGroup = GetComponent<VerticalLayoutGroup>();
            contentSizeFitter = GetComponent<ContentSizeFitter>();

            // =========================
            // VerticalLayoutGroup 설정
            // =========================

            // 자식의 가로 크기를 부모가 제어
            verticalGroup.childControlWidth = true;

            // 자식의 세로 크기는 부모가 강제로 변경하지 않음
            verticalGroup.childControlHeight = false;

            // 자식을 가로로 꽉 채움
            verticalGroup.childForceExpandWidth = true;

            // 세로 방향 강제 확장 안 함
            verticalGroup.childForceExpandHeight = false;

            // =========================
            // ContentSizeFitter 설정
            // =========================

            // 가로 크기는 자동 조절 안 함
            contentSizeFitter.horizontalFit =
                ContentSizeFitter.FitMode.Unconstrained;

            // 세로 크기는 내용에 맞게 자동 조절
            contentSizeFitter.verticalFit =
                ContentSizeFitter.FitMode.PreferredSize;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 새로운 Bullet Point 항목 추가
        /// </summary>
        /// <param name="text">표시할 텍스트</param>
        /// <returns>생성된 BulletPointItem</returns>
        public BulletPointItem AddBulletPoint(string text)
        {
            // 프리팹이 연결되지 않은 경우
            if (itemPrefab == null)
            {
                Debug.LogError(
                    "BulletPoint : Item Prefab이 연결되지 않았습니다."
                );

                return null;
            }

            // 프리팹을 현재 오브젝트의 자식으로 생성
            BulletPointItem item =
                Instantiate(itemPrefab, transform);

            // 텍스트 설정
            item.Text = text;

            // 글자 색상 적용
            item.Color = textColor;

            // 글자 크기 적용
            item.Size = textSize;

            // 생성된 항목 반환
            return item;
        }

        /// <summary>
        /// 모든 Bullet Point 삭제
        /// </summary>
        public void ClearAll()
        {
            // 자식 오브젝트를 뒤에서부터 삭제
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                // 에디터와 플레이 모드 모두 지원
                DestroyImmediate(
                    transform.GetChild(i).gameObject
                );
            }
        }

        #endregion
    }
}