/*
 * MapEditor
 * Пользователь: AngryKirC
 * Дата: 14.04.2015
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MapEditor.noxscript2
{
	/// <summary>
	/// RichTextBox overload that also provides highlighting functionality
	/// </summary>
	public class CodeTextBox : RichTextBox
	{
		public void BeginUpdate() 
		{
	        SendMessage(this.Handle, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
	    }
		
	    public void EndUpdate() 
	    {
	        SendMessage(this.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero); 
	        this.Invalidate();
	    }
		
	    [DllImport("user32.dll")]
	    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
	    private const int WM_SETREDRAW = 0x0b;
		
		ScriptObjContainer scripts = null;
		
		/* Color defintions */
		public Color DefaultColor = Color.Black;
		public Color ErrorColor = Color.Red;

		public Color NoxFunctionColor = Color.DarkBlue;
		public Color UserFunctionColor = Color.DarkViolet;

		public Color StringColor = Color.Green;
		
		public CodeTextBox()
		{
			// Setup font
			Font = EditorSettings.Default.Script_CodeFont;
		}
		
		public void SetScriptContainer(ScriptObjContainer soc)
		{
			scripts = soc;
		}
		
		private bool IsVariableName(string text)
		{
			foreach (ScriptObjContainer.ScriptFunction.ScriptVariable sv in scripts.Functions[1].Variables)
			{
				if (sv.Name == text)
				{
					return true;
				}
			}
			return false;
		}
		
		private void ColorTextArea(int startIndex, int length, Color color)
		{
			Select(startIndex, length);
			SelectionColor = color;
		}
		
		/// <summary>
		/// Highlight a function call
		/// </summary>
		private void ColorFunction(string name, int fn)
		{
			// In case that function does not exist
			Color hltColor = ErrorColor;
			
			// Check if it's a Nox function
			foreach (string nf in NoxFuncs.FunctionNames)
			{
				if (nf == name)
				{
					hltColor = NoxFunctionColor;
					break;
				}
			}
			
			// Check if it's a user declared function
			foreach (ScriptObjContainer.ScriptFunction sf in scripts.Functions)
			{
				if (sf.Name == name)
				{
					hltColor = UserFunctionColor;
					break;
				}
			}
			
			ColorTextArea(fn, name.Length, hltColor);
		}

		/// <summary>
		/// TextChanged hook. Format script syntax
		/// </summary>
		protected override void OnTextChanged(EventArgs e)
		{
			BeginUpdate();
			
			string codeString = Text;
			string chunk = "";
			int charIndex = 0;
			int lineCounter = 0;
			char nextChar;
			bool handled = false;
			bool rstring = false;
			int stringIndex = 0;
			
			int css = SelectionStart;
			int csl = SelectionLength;
			// reset coloring for entire text
			Select(0, Text.Length);
			SelectionColor = DefaultColor;
			
			while (codeString.Length > charIndex)
			{
				nextChar = codeString[charIndex];
				charIndex++;
				
				handled = true;
				if (chunk == "if" || chunk == "jump" || chunk == "return")
				{
					//int fn = charIndex - chunk.Length - 1;
					//ColorTextArea(fn, chunk.Length, KeywordColor);
				}
				else
				{
					switch (nextChar)
					{
						case '\n':
						case '\r':
							lineCounter++;
							break;
						case '(':
							// function call start
							int fn = charIndex - chunk.Length - 1;
							ColorFunction(chunk, fn);
							break;
						case ',':
							// argument separator
							break;
						case ')':
							// function call end
							break;
						case '=':
						case '-':
						case '+':
						case '*':
						case '/':
						case '<':
						case '>':
						case '%':
						case '&':
						case '^':
						case '|':
							// handled
							break;
						case '"':
							if (rstring)
							{
								// Finish string
								int len = charIndex - stringIndex;
								ColorTextArea(stringIndex, len, StringColor);
								rstring = false;
							}
							else
							{
								// Start string
								stringIndex = charIndex - 1;
								rstring = true;
							}
							break;
						default:
							handled = false;
							break;
					}
				}
				
				if (handled)
					chunk = "";
				else if (nextChar != ' ')
					chunk += nextChar;
			}
			
			// reset selection
			Select(css, csl);
			
			EndUpdate();
			base.OnTextChanged(e);
		}
	}
}
