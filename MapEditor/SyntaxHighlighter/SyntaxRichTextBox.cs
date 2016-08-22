using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;

namespace SyntaxHighlighter
{
	public class SyntaxRichTextBox : System.Windows.Forms.RichTextBox
	{
		private SyntaxSettings m_settings = new SyntaxSettings();
		public bool m_bPaint = true;
		private string m_strLine = "";
		private int m_nLineStart = 0;
		private int m_nLineEnd = 0;
        bool recur = false;

		/// <summary>
		/// The settings.
		/// </summary>
		public SyntaxSettings Settings
		{
			get { return m_settings; }
		}
		
		/// <summary>
		/// WndProc
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if (m.Msg == 0x00f)
			{
				if (m_bPaint)
					base.WndProc(ref m);
				else
					m.Result = IntPtr.Zero;
			}
			else
				base.WndProc(ref m);
		}
		/// <summary>
		/// OnTextChanged
		/// </summary>
		/// <param name="e"></param>
		protected override void OnTextChanged(EventArgs e)
		{
            if (!m_bPaint)
                return;
            m_bPaint = false;
                ProcessLine();
			m_bPaint = true;
		}
		/// <summary>
		/// Process a line.
		/// </summary>
		private void ProcessLine()
		{
           
            if (recur)
                return;

            int start = 0;
            //SelectionColor = Color.White;
            recur = true;
            int selstart = GetFirstCharIndexOfCurrentLine();
            int indx = GetLineFromCharIndex(selstart);
            if (indx == 0 && selstart == 0 && Text.Length < 1)
            {
                SelectionStart = 0;
            goto sem;
            } 
            start = Lines[indx].Length;
            int lastpos = SelectionStart;
            
            
            int selend = selstart + start;

            SelectionStart = selstart;
            SelectionLength = start;
            SelectionColor = Color.Black;

            List<string> strs = Settings.Keywords2;
            Color KeyCol = Settings.Keyword2Color;
            foreach (string str in strs)
            {
                if (selend > selstart)
                    ColorText(str, selstart, selend, KeyCol);
            }

            strs = Settings.Keywords;
            KeyCol = Settings.KeywordColor;
            foreach (string str in strs)
            {
                if (selend > selstart)
                    ColorText(str, selstart, selend, KeyCol);
            }

            if (selend > selstart)
            {

                int texstart = Find("\"", selstart, selend, RichTextBoxFinds.None);
                if (texstart > 0)
                {
                    int texend = Find("\"", texstart+1, selend, RichTextBoxFinds.None);
                    if (texend > 0)
                    {
                        int len = texend - texstart;
                        if (len > 0)
                        {
                            SelectionStart = texstart;
                            SelectionLength = len + 1;
                            SelectionColor = Settings.StringColor;
                        }
                    }
                }

                texstart = Find(Settings.Comment, selstart, selend, RichTextBoxFinds.None);
                if (texstart > 0)
                {
                    int texend = selend;
                    int len = texend - texstart;
                    if (len > 0)
                    {
                        SelectionStart = texstart;
                        SelectionLength = len;
                        SelectionColor = Settings.CommentColor;
                    }
                }
            }

            
    
    SelectionStart = lastpos;
    sem:
    recur = false;
    SelectionLength = 0;

    
    SelectionColor = Color.Black; // Selected text



}

public void ProcessBox()
{
    int start = 0;
    foreach (string str in Lines)
    {
        SelectionStart = start;
        ProcessLine();
        start += str.Length+1;
    }
    SelectionStart = start;
    ProcessLine();
    SelectionStart = 0;
    SelectionLength = 0;
    return;
}
/// <summary>
/// Process a regular expression.
/// </summary>
/// <param name="strRegex">The regular expression.</param>
/// <param name="color">The color.</param>
        private void ProcessRegex(string strRegex, Color color)
        {

        }
		/// <summary>
		/// Compiles the keywords as a regular expression.
		/// </summary>
		public void CompileKeywords()
		{
		}
        public int ColorText(string searchText, int searchStart, int searchEnd, Color col)
        {

            // Initialize the return value to false by default.
            int returnValue = -1;
            int indexToText = 0;
            // Ensure that a search string and a valid starting point are specified.
            if (searchText.Length > 0 && searchStart >= 0)
            {
                // Ensure that a valid ending value is provided.
                while (indexToText >= 0 && searchEnd > searchStart)
                {
                    // Obtain the location of the search string in richTextBox1.
                    indexToText = Find(searchText, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexToText >= 0)
                    {
                        // Return the index to the specified search text.
                        searchStart = indexToText + SelectionLength;
                        SelectionColor = col;
                        returnValue = indexToText;
                    }
                }

            }
            return returnValue;
        }
		public void ProcessCurLine()
		{
			m_bPaint = false;

			int nStartPos = SelectionStart;
			int nOriginalPos = SelectionStart;
            
				m_strLine = Lines[GetLineFromCharIndex(nStartPos)];
				m_nLineStart = nStartPos;
				m_nLineEnd = m_nLineStart + m_strLine.Length;

				ProcessLine();

			m_bPaint = true;
            SelectionStart = nOriginalPos;
	}
		public void ProcessAllLines()
		{
		}
	}

	/// <summary>
	/// Class to store syntax objects in.
	/// </summary>
	public class SyntaxList // ?
	{
		public List<string> m_rgList = new List<string>();
		public Color m_color = new Color();
	}

	/// <summary>
	/// Settings for the keywords and colors.
	/// </summary>
	public class SyntaxSettings
	{
		SyntaxList m_rgKeywords = new SyntaxList();
        SyntaxList m_rgKeywords2 = new SyntaxList();
		string m_strComment = "";
		Color m_colorComment = Color.Green;
		Color m_colorString = Color.Gray;
		Color m_colorInteger = Color.Red;
		bool m_bEnableComments = true;
		bool m_bEnableIntegers = true;
		bool m_bEnableStrings = true;

		#region Properties
		/// <summary>
		/// A list containing all keywords.
		/// </summary>
		public List<string> Keywords
		{
			get { return m_rgKeywords.m_rgList; }
		}
        public List<string> Keywords2
        {
            get { return m_rgKeywords2.m_rgList; }
        }
		/// <summary>
		/// The color of keywords.
		/// </summary>
		public Color KeywordColor
		{
			get { return m_rgKeywords.m_color; }
			set { m_rgKeywords.m_color = value; }
		}
        public Color Keyword2Color
        {
            get { return m_rgKeywords2.m_color; }
            set { m_rgKeywords2.m_color = value; }
        }
		/// <summary>
		/// A string containing the comment identifier.
		/// </summary>
		public string Comment
		{
			get { return m_strComment; }
			set { m_strComment = value; }
		}
		/// <summary>
		/// The color of comments.
		/// </summary>
		public Color CommentColor
		{
			get { return m_colorComment; }
			set { m_colorComment = value; }
		}
		/// <summary>
		/// Enables processing of comments if set to true.
		/// </summary>
		public bool EnableComments
		{
			get { return m_bEnableComments; }
			set { m_bEnableComments = value; }
		}
		/// <summary>
		/// Enables processing of integers if set to true.
		/// </summary>
		public bool EnableIntegers
		{
			get { return m_bEnableIntegers; }
			set { m_bEnableIntegers = value; }
		}
		/// <summary>
		/// Enables processing of strings if set to true.
		/// </summary>
		public bool EnableStrings
		{
			get { return m_bEnableStrings; }
			set { m_bEnableStrings = value; }
		}
		/// <summary>
		/// The color of strings.
		/// </summary>
		public Color StringColor
		{
			get { return m_colorString; }
			set { m_colorString = value; }
		}
		/// <summary>
		/// The color of integers.
		/// </summary>
		public Color IntegerColor
		{
			get { return m_colorInteger; }
			set { m_colorInteger = value; }
		}
		#endregion
	}
}
