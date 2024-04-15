// ERROR_AMBIGUOUS_REFERENCE_TYPE
language core;
extend with #unit-type, #references, #let-bindings, #sequencing, #natural-literals;

fn main(n : Nat) -> Nat {
    return <0x123>
}