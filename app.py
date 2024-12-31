import gradio as gr
import subprocess


def puzzleRun(input_text):
    result = subprocess.run(
        ["dotnet", "run", "Program", "--", input_text.replace(",", "")],
        capture_output=True,
        text=True,
    ).stdout
    return result


textbox = gr.Textbox(
    label="Path List (swipe/drag to scrolldown)", show_copy_button=True
)

iface = gr.Interface(
    fn=puzzleRun,
    inputs=gr.Textbox(lines=1, label="planet list + home planet"),
    outputs=textbox,
    examples=["6,2,2,4,4,4,3,2,8","7,8,6,1,1,6,7,7,4"],
    title="Quanta Puzzle Solver",
)

iface.launch(share=True)
