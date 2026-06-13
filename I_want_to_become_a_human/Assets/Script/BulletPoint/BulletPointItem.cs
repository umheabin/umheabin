using TMPro;              // TextMeshProUGUI 사용
using UnityEngine;
using UnityEngine.UI;     // ContentSizeFitter 사용

namespace BulletPoint
{
    // ▶ 이 스크립트가 붙은 오브젝트에는
    //    반드시 TextMeshProUGUI 컴포넌트가 있어야 함
    [RequireComponent(typeof(TextMeshProUGUI))]

    // ▶ 이 스크립트가 붙은 오브젝트에는
    //    반드시 ContentSizeFitter 컴포넌트가 있어야 함
    [RequireComponent(typeof(ContentSizeFitter))]
    public class BulletPointItem : MonoBehaviour
    {
        #region Private Variables

        // ▶ 글머리표 문자
        //    기본값 : •
        [SerializeField]
        private char bulletCharacter = '•';

        // ▶ TextMeshProUGUI 컴포넌트 캐싱
        private TextMeshProUGUI textField;

        // ▶ ContentSizeFitter 컴포넌트 캐싱
        private ContentSizeFitter fitter;

        #endregion

        #region Properties

        /// <summary>
        /// 불릿 텍스트 설정
        /// </summary>
        public string Text
        {
            get
            {
                // 현재 텍스트 반환
                return textField.text;
            }

            set
            {
                // 글머리표 + 공백 + 실제 텍스트
                textField.text = "* " + value;
            }
        }

        /// <summary>
        /// 텍스트 색상 설정
        /// </summary>
        public Color Color
        {
            get
            {
                return textField.color;
            }

            set
            {
                textField.color = value;
            }
        }

        /// <summary>
        /// 글자 크기 설정
        /// </summary>
        public float Size
        {
            get
            {
                return textField.fontSize;
            }

            set
            {
                textField.fontSize = value;
            }
        }

        #endregion

        #region Unity Methods

        // ▶ 오브젝트가 생성될 때 호출
        private void Awake()
        {
            // TextMeshProUGUI 가져오기
            textField =
                GetComponent<TextMeshProUGUI>();

            // ContentSizeFitter 가져오기
            fitter =
                GetComponent<ContentSizeFitter>();

            // =========================
            // ContentSizeFitter 설정
            // =========================

            // 가로 크기는 자동 조절 안 함
            fitter.horizontalFit =
                ContentSizeFitter.FitMode.Unconstrained;

            // 세로 크기는 텍스트 크기에 맞게 자동 조절
            fitter.verticalFit =
                ContentSizeFitter.FitMode.PreferredSize;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 글머리표 문자 변경
        /// 예: • → ★ → ✓
        /// </summary>
        public void SetBulletCharacter(char newBullet)
        {
            bulletCharacter = newBullet;

            // 기존 텍스트가 있다면 다시 적용
            string currentText = textField.text;

            // 앞의 글머리표 제거 후 다시 설정
            if (currentText.Length > 2)
            {
                Text = currentText.Substring(2);
            }
        }

        /// <summary>
        /// 체크 완료 상태로 변경
        /// </summary>
        public void MarkAsCompleted()
        {
            bulletCharacter = '✓';

            // 기존 텍스트 유지하면서 체크 표시로 변경
            string currentText = textField.text;

            if (currentText.Length > 2)
            {
                Text = currentText.Substring(2);
            }
        }

        #endregion
    }
}