import gradio as gr
import subprocess

def puzzleRun(input_text):
    result = subprocess.run(['dotnet','run','--',input_text.replace(',','')], capture_output=True, text=True).stdout
    return result

textbox = gr.Textbox(label="Path List (swipe/drag to scrolldown)")
textbox.style(show_copy_button=True)

iface = gr.Interface(fn=puzzleRun,
                     inputs=gr.inputs.Textbox(lines=1, label="planet list"),
                     outputs=textbox,
                     examples=["78623484"],
                     title="Quanta Puzzle Solver")


iface.launch(share=True)
