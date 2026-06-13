using UnityEditor;     // Unity Editor 기능 사용
using UnityEngine;

namespace BulletPoint
{
    // ▶ BulletPoint 컴포넌트의 Inspector를 커스터마이징
    //    BulletPoint를 선택하면 기본 Inspector 대신
    //    이 Editor 스크립트가 실행됨
    [CustomEditor(typeof(BulletPoint))]
    public class BulletPointEditor : Editor
    {
        #region Variables

        // ▶ 새로 추가할 불릿 텍스트를 임시 저장
        private string newItemText = "";

        #endregion

        #region Unity Methods

        /// <summary>
        /// Inspector를 그리는 함수
        /// BulletPoint를 선택할 때마다 호출됨
        /// </summary>
        public override void OnInspectorGUI()
        {
            // ==================================
            // 기본 Inspector 출력
            // ==================================

            // BulletPoint의 기본 필드 표시
            // (textColor, textSize, itemPrefab)
            DrawDefaultInspector();

            // 현재 선택된 BulletPoint 가져오기
            BulletPoint bulletPoint =
                (BulletPoint)target;

            // Inspector에 빈 공간 추가
            EditorGUILayout.Space();

            // ==================================
            // Update All Points 버튼
            // ==================================

            if (GUILayout.Button(
                "Update All Points",
                GUILayout.Height(30)))
            {
                // 자식으로 존재하는 모든 BulletPointItem 찾기
                foreach (BulletPointItem item in
                    bulletPoint.GetComponentsInChildren<BulletPointItem>())
                {
                    // 부모 설정값으로 통일
                    item.Color = bulletPoint.textColor;
                    item.Size = bulletPoint.textSize;

                    // 변경 사항 저장
                    EditorUtility.SetDirty(item);
                }

                Debug.Log(
                    "모든 BulletPointItem의 스타일이 업데이트되었습니다.");
            }

            // Inspector에 빈 공간 추가
            EditorGUILayout.Space();

            // ==================================
            // 새 항목 추가 영역
            // ==================================

            // 제목 표시
            EditorGUILayout.LabelField(
                "Add New Bullet Point",
                EditorStyles.boldLabel);

            // 설명 문구 표시
            EditorGUILayout.HelpBox(
                "추가할 텍스트를 입력한 후 Add To List 버튼을 누르세요.",
                MessageType.Info);

            // 여러 줄 입력 가능한 텍스트 박스
            newItemText =
                EditorGUILayout.TextArea(
                    newItemText,
                    GUILayout.MinHeight(80));

            // ==================================
            // Add To List 버튼
            // ==================================

            if (GUILayout.Button(
                "Add To List",
                GUILayout.Height(30)))
            {
                // 공백만 입력된 경우 생성 금지
                if (!string.IsNullOrWhiteSpace(newItemText))
                {
                    // Undo 기록 저장
                    // Ctrl + Z 가능하게 해줌
                    Undo.RecordObject(
                        bulletPoint.gameObject,
                        "Add Bullet Point");

                    // BulletPointItem 생성
                    bulletPoint.AddBulletPoint(
                        newItemText);

                    // 입력창 비우기
                    newItemText = "";

                    // 씬 저장 플래그 설정
                    EditorUtility.SetDirty(
                        bulletPoint);

                    Debug.Log(
                        "새 Bullet Point가 추가되었습니다.");
                }
                else
                {
                    Debug.LogWarning(
                        "추가할 텍스트를 입력하세요.");
                }
            }

            // ==================================
            // 전체 삭제 버튼
            // ==================================

            EditorGUILayout.Space();

            if (GUILayout.Button(
                "Clear All Points",
                GUILayout.Height(30)))
            {
                // 삭제 확인 창
                bool delete =
                    EditorUtility.DisplayDialog(
                        "Delete All",
                        "정말 모든 Bullet Point를 삭제하시겠습니까?",
                        "삭제",
                        "취소");

                if (delete)
                {
                    // Undo 기록 저장
                    Undo.RecordObject(
                        bulletPoint.gameObject,
                        "Clear Bullet Points");

                    // 전체 삭제
                    bulletPoint.ClearAll();

                    // 저장 플래그
                    EditorUtility.SetDirty(
                        bulletPoint);

                    Debug.Log(
                        "모든 Bullet Point가 삭제되었습니다.");
                }
            }

            // ==================================
            // 하단 안내 메시지
            // ==================================

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(
                "추가되는 항목은 위에서 설정한 색상과 크기를 사용합니다.",
                MessageType.Warning);
        }

        #endregion
    }
}