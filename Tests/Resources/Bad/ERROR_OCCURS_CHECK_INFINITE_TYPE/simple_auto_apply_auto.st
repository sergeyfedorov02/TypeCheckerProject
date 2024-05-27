language core;
extend with #type-reconstruction, #multiparameter-functions;

fn main(f : auto) -> auto {
    return f(f)
}